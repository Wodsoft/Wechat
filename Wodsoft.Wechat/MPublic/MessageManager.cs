using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class MessageManager
    {
        public event MessageHandler<IUserSubscribeEvent> Subscribe;
        public event MessageHandler<IUserScanEvent> Scan;
        public event MessageHandler<IUserLocatedEvent> Locate;
        public event MessageHandler<IUserClickEvent> Click;
        public event MessageHandler<IUserTemplateSendEvent> TemplateSend;

        public event MessageHandler<IUserTextMessage> ReceiveText;
        public event MessageHandler<IUserImageMessage> ReceiveImage;
        public event MessageHandler<IUserVoiceMessage> ReceiveVoice;
        public event MessageHandler<IUserVideoMessage> ReceiveVideo;
        public event MessageHandler<IUserShortVideoMessage> ReceiveShortVideo;
        public event MessageHandler<IUserLocationMessage> ReceiveLocation;
        public event MessageHandler<IUserLinkMessage> ReceiveLink;

        private ConcurrentDictionary<int, SemaphoreSlim> _Locker;

        public MessageManager()
        {
            _Locker = new ConcurrentDictionary<int, SemaphoreSlim>();
        }

        public virtual async Task<IReplyMessage> ExecuteMessage(IDictionary<string, string> dictionary)
        {
            string type = dictionary["MsgType"];
            IReplyMessage message;
            if (type == "event")
            {
                type = dictionary["Event"].ToLower();
                message = await OnReceiveEvent(type, dictionary);
            }
            else if (dictionary.ContainsKey("MsgId"))
            {
                message = await OnReceiveMessage(type, dictionary);
                if (message == null && DefaultReplyMessage != null)
                    message = new ReplyTextMessage
                    {
                        ToUser = dictionary["FromUserName"],
                        FromUser = dictionary["ToUserName"],
                        Content = DefaultReplyMessage
                    };
            }
            else
                throw new UnknownMessageException(type);
            return message;
        }

        public string DefaultReplyMessage { get; set; }

        protected async Task<bool> TryEnterLock(int hashKey)
        {
            SemaphoreSlim locker = _Locker.GetOrAdd(hashKey, new SemaphoreSlim(1));
            return await locker.WaitAsync(0);
        }

        protected void ExitLock(int hashKey)
        {
            Task.Run(async () =>
            {
                await Task.Delay(15000);
                SemaphoreSlim locker;
                _Locker.TryRemove(hashKey, out locker);
                locker.Release();
            });
        }

        protected virtual async Task<IReplyMessage> OnReceiveEvent(string type, IDictionary<string, string> dictionary)
        {
            if (!await TryEnterLock(dictionary["FromUserName"].GetHashCode() + dictionary["CreateTime"].GetHashCode()))
                return new ReplyEmptyMessage();
            try
            {
                switch (type)
                {
                    case "subscribe":
                    case "unsubscribe":
                        if (dictionary.ContainsKey("Ticket"))
                        {
                            if (Scan != null)
                            {
                                var e = new UserScanEvent(dictionary);
                                var arg = new MessageHandlerEventArgs<IUserScanEvent>(e);
                                await Scan(this, arg);
                                return arg.Reply;
                            }
                        }
                        else
                        {
                            if (Subscribe != null)
                            {
                                var e = new UserSubscribeEvent(dictionary);
                                var arg = new MessageHandlerEventArgs<IUserSubscribeEvent>(e);
                                await Subscribe(this, arg);
                                return arg.Reply;
                            }
                        }
                        return null;
                    case "scan":
                        if (Scan != null)
                        {
                            var e = new UserScanEvent(dictionary);
                            var arg = new MessageHandlerEventArgs<IUserScanEvent>(e);
                            await Scan(this, arg);
                            return arg.Reply;
                        }
                        return null;
                    case "location":
                        if (Locate != null)
                        {
                            var e = new UserLocatedEvent(dictionary);
                            var arg = new MessageHandlerEventArgs<IUserLocatedEvent>(e);
                            await Locate(this, arg);
                            return arg.Reply;
                        }
                        return null;
                    case "click":
                        if (Click != null)
                        {
                            var e = new UserClickEvent(dictionary);
                            var arg = new MessageHandlerEventArgs<IUserClickEvent>(e);
                            await Click(this, arg);
                            return arg.Reply;
                        }
                        return null;
                    case "templatesendjobfinish":
                        if (TemplateSend != null)
                        {
                            var e = new UserTemplateSendEvent(dictionary);
                            var arg = new MessageHandlerEventArgs<IUserTemplateSendEvent>(e);
                            await TemplateSend(this, arg);
                            return arg.Reply;
                        }
                        return null;
                    default:
                        throw new UnknownMessageException(type);
                }
            }
            finally
            {
                ExitLock(dictionary["FromUserName"].GetHashCode() + dictionary["CreateTime"].GetHashCode());
            }
        }

        protected virtual async Task<IReplyMessage> OnReceiveMessage(string type, IDictionary<string, string> dictionary)
        {
            if (!await TryEnterLock(dictionary["MsgId"].GetHashCode()))
                return new ReplyEmptyMessage();
            try
            {
                if (type == "text")
                {
                    if (ReceiveText == null)
                        return null;
                    var e = new UserTextMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserTextMessage>(e);
                    await ReceiveText(this, arg);
                    return arg.Reply;
                }
                else if (type == "image")
                {
                    if (ReceiveImage == null)
                        return null;
                    var e = new UserImageMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserImageMessage>(e);
                    await ReceiveImage(this, arg);
                    return arg.Reply;
                }
                else if (type == "voice")
                {
                    if (ReceiveVoice == null)
                        return null;
                    var e = new UserVoiceMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserVoiceMessage>(e);
                    await ReceiveVoice(this, arg);
                    return arg.Reply;
                }
                else if (type == "video")
                {
                    if (ReceiveVideo == null)
                        return null;
                    var e = new UserVideoMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserVideoMessage>(e);
                    await ReceiveVideo(this, arg);
                    return arg.Reply;
                }
                else if (type == "shortvideo")
                {
                    if (ReceiveShortVideo == null)
                        return null;
                    var e = new UserShortVideoMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserShortVideoMessage>(e);
                    await ReceiveShortVideo(this, arg);
                    return arg.Reply;
                }
                else if (type == "location")
                {
                    if (ReceiveLocation == null)
                        return null;
                    var e = new UserLocationMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserLocationMessage>(e);
                    await ReceiveLocation(this, arg);
                    return arg.Reply;
                }
                else if (type == "link")
                {
                    if (ReceiveLink == null)
                        return null;
                    var e = new UserLinkMessage(dictionary);
                    var arg = new MessageHandlerEventArgs<IUserLinkMessage>(e);
                    await ReceiveLink(this, arg);
                    return arg.Reply;
                }
                else
                    throw new UnknownMessageException(type);
            }
            finally
            {
                ExitLock(dictionary["MsgId"].GetHashCode());
            }
        }
    }

    public delegate Task MessageHandler<T>(MessageManager sender, MessageHandlerEventArgs<T> e) where T : IMessage;

    public class MessageHandlerEventArgs<T>
        where T : IMessage
    {
        public MessageHandlerEventArgs(T e)
        {
            Event = e;
        }

        /// <summary>
        /// 获取事件内容。
        /// </summary>
        public T Event { get; private set; }

        /// <summary>
        /// 获取或设置回复消息。
        /// </summary>
        public IReplyMessage Reply { get; set; }
    }
}
