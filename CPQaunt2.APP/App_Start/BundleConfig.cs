﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CPQaunt2.APP
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                     "~/Resources/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/wui").Include(
                       "~/Resources/Scripts/wui/wui.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resources/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Resources/Scripts/bootstrap.js",
                      "~/Resources/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Resources/css/stylebase.css"));

            // 启用合并压缩
            BundleTable.EnableOptimizations = true;
        }
    }
}