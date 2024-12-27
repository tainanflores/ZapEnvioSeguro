using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ZapEnvioSeguro.Entidades
{
    public static class Parametros
    {
        static List<WhatsAppMessage> lista = new List<WhatsAppMessage>();
        public static List<WhatsAppMessage> NovaMensagem(string json)
        {
            lista = new List<WhatsAppMessage>();
            WhatsAppMessage response = JsonConvert.DeserializeObject<WhatsAppMessage>(json);

            lista.Add(response);
            return lista;
        }
    }

    // Classes para deserializar a resposta da API dos Correios
    public class WhatsAppMessage
    {
        public Id Id { get; set; }
        public bool Viewed { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public long T { get; set; }
        public string NotifyName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Ack { get; set; }
        public bool Invis { get; set; }
        public bool IsNewMsg { get; set; }
        public bool Star { get; set; }
        public bool KicNotified { get; set; }
        public bool RecvFresh { get; set; }
        public bool IsFromTemplate { get; set; }
        public string Thumbnail { get; set; }
        public bool PollInvalidated { get; set; }
        public bool IsSentCagPollCreation { get; set; }
        public object LatestEditMsgKey { get; set; }
        public object LatestEditSenderTimestampMs { get; set; }
        public object[] MentionedJidList { get; set; }
        public object[] GroupMentions { get; set; }
        public bool IsEventCanceled { get; set; }
        public bool EventInvalidated { get; set; }
        public bool IsVcardOverMmsDocument { get; set; }
        public object[] Labels { get; set; }
        public bool HasReaction { get; set; }
        public int EphemeralDuration { get; set; }
        //public int EphemeralSettingTimestamp { get; set; }
        public string DisappearingModeInitiator { get; set; }
        public string DisappearingModeTrigger { get; set; }
        public bool ProductHeaderImageRejected { get; set; }
        public int LastPlaybackProgress { get; set; }
        public bool IsDynamicReplyButtonsMsg { get; set; }
        public bool IsCarouselCard { get; set; }
        public object ParentMsgId { get; set; }
        public bool IsMdHistoryMsg { get; set; }
        public long StickerSentTs { get; set; }
        public bool IsAvatar { get; set; }
        public int LastUpdateFromServerTs { get; set; }
        public object InvokedBotWid { get; set; }
        public object BizBotType { get; set; }
        public object BotResponseTargetId { get; set; }
        public object BotPluginType { get; set; }
        public object BotPluginReferenceIndex { get; set; }
        public object BotPluginSearchProvider { get; set; }
        public object BotPluginSearchUrl { get; set; }
        public bool BotPluginMaybeParent { get; set; }
        public object BotReelPluginThumbnailCdnUrl { get; set; }
        public object BotMsgBodyType { get; set; }
        public object RequiresDirectConnection { get; set; }
        public object BizContentPlaceholderType { get; set; }
        public bool HostedBizEncStateMismatch { get; set; }
        public bool SenderOrRecipientAccountTypeHosted { get; set; }
        public bool PlaceholderCreatedWhenAccountIsHosted { get; set; }
    }

    public class Id
    {
        public bool FromMe { get; set; }
        public string Remote { get; set; }
        public string id { get; set; }
        public string _Serialized { get; set; }
    }

}
