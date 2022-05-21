﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RPNL.Net.Utilities.WebUtils
{
    public class BootstrapModel
    {
        public string ID { get; set; }
        public string AreaLabeledId { get; set; }
        public ModalSize Size { get; set; }
        public string Message { get; set; }
        public string ModalSizeClass
        {
            get
            {
                switch (this.Size)
                {
                    case ModalSize.Small:
                        return "modal-sm";
                    case ModalSize.Large:
                        return "modal-lg";
                    case ModalSize.Medium:
                    default:
                        return "";
                }
            }
        }

        public class ModalFooter
        {
            public string SubmitButtonText { get; set; } = "Submit";
            public string CancelButtonText { get; set; } = "Cancel";
            public string SubmitButtonID { get; set; } = "btn-submit";
            public string CancelButtonID { get; set; } = "btn-cancel";
            public bool OnlyCancelButton { get; set; }
            public string SubmitType { get; set; } = "submit";
        }

        public enum ModalSize
        {
            Small,
            Large,
            Medium
        }

        public class ModalHeader
        {
            public string Heading { get; set; }
        }
    }
}