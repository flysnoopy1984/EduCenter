﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EduCenterModel.WX
{
    public class WXMessage
    {
        /// <summary>
        /// 消息接收方微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息发送方微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 信息类型 地理位置:location,文本消息:text,消息类型:image
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 图片链接，开发者可以用HTTP GET获取
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 事件类型，subscribe(订阅/扫描带参数二维码订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件) 、SCAN（已关注的状态下扫描带参数二维码）
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可以用来换取二维码
        /// </summary>
        public string Ticket { get; set; }

        public string toText(string content)
        {
            string xml = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[{3}]]></MsgType>
                                <Content><![CDATA[{4}]]></Content>
                            </xml>";
            xml = string.Format(xml, this.FromUserName, this.ToUserName, 12345678, "text", content);
            return xml;
        }

        public string toPic(string mediaId)
        {
            string xml = @"<xml>
                      <ToUserName>
                        <![CDATA[{0}]]></ToUserName>
                      <FromUserName>
                        <![CDATA[{1}]]></FromUserName>
                      <CreateTime>12345678</CreateTime>
                      <MsgType>
                        <![CDATA[image]]></MsgType>
                      <Image>
                        <MediaId>
                          <![CDATA[{2}]]></MediaId>
                      </Image>
                    </xml>";
            xml = string.Format(xml, this.FromUserName, this.ToUserName, mediaId);
            return xml;

        }

        public string toPicText(string picUrl, string url, string desc = "点击获取酷炫二维码标记", string title = "收款二维码")
        {

            string xml = @"<xml>
                <ToUserName><![CDATA[{0}]]></ToUserName>
                <FromUserName><![CDATA[{1}]]></FromUserName>
                <CreateTime>12345678</CreateTime>
                <MsgType><![CDATA[news]]></MsgType>
                <ArticleCount>1</ArticleCount>
                <Articles>
                <item>
                <Title><![CDATA[{5}]]></Title> 
                <Description><![CDATA[{4}]]></Description>
                <PicUrl><![CDATA[{2}]]></PicUrl>
                <Url><![CDATA[{3}]]></Url>
                </item>
                </Articles>
                </xml>";
            xml = string.Format(xml, this.FromUserName, this.ToUserName, picUrl, url, desc, title);
            return xml;
        }



        public void LoadXml(string xml)
        {

            XmlNode node = null;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            XmlElement rootele = xmldoc.DocumentElement;
            node = rootele.SelectSingleNode("ToUserName");
            if (node != null)
                this.ToUserName = node.InnerText;

            node = rootele.SelectSingleNode("FromUserName");
            if (node != null)
                this.FromUserName = node.InnerText;

            node = rootele.SelectSingleNode("MsgType");
            if (node != null)
                this.MsgType = node.InnerText;

            node = rootele.SelectSingleNode("Event");
            if (node != null)
                this.Event = node.InnerText.ToLower();

            node = rootele.SelectSingleNode("EventKey");
            if (node != null)
                this.EventKey = node.InnerText;

            node = rootele.SelectSingleNode("Ticket");
            if (node != null)
                this.Ticket = node.InnerText;

        }
    }
}
