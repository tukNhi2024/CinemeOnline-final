﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaOnline.Models
{
    public class ResponsePayment
    {
        public string PartnerCode { get; set; }

        public string AccessKey { get; set; }

        public string RequestId { get; set; }

        public string Amount { get; set; }

        public string OrderId { get; set; }

        public string OrderInfo { get; set; }

        public string OrderType { get; set; }

        public string TransId { get; set; }

        public string Message { get; set; }

        public string LocalMessage { get; set; }

        public string ResponseTime { get; set; }

        public string ErrorCode { get; set; }

        public string PayType { get; set; }

        public string ExtraData { get; set; }

        public string Signature { get; set; }
    }
}