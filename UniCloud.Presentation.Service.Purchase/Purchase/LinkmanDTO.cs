﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class LinkmanDTO
    {
        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("名称不为空");
            }
        }
    }
}
