﻿#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
// 
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteveCadwallader.CodeMaid.Properties;
using System;

namespace SteveCadwallader.CodeMaid.UnitTests.Formatting
{
    /// <summary>
    /// Class with simple unit tests for formatting header type comments. This calls the formatter
    /// directly, rather than invoking it through the UI as with the integration tests.
    /// </summary>
    [TestClass]
    public class HeaderFormattingTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Settings.Default.Reset();
        }

        /// <summary>
        /// Tests the forced indenting of the XML copyright file header.
        /// </summary>
        [TestMethod]
        [TestCategory("Formatting UnitTests")]
        public void HeaderFormattingTests_IndentsXML()
        {
            Settings.Default.Formatting_CommentXmlValueIndent = 0;

            var input =
                @"<copyright file=""NameOfFile.cs"" company=""CompanyName"">" + Environment.NewLine +
                @"Company copyright tag." + Environment.NewLine +
                @"</copyright>";

            // It's not fair, but even though we are formatting without a prefix, the forced header
            // indenting adds one extra space (to separate the prefix from the text) to the
            // indenting, making it 5 total.
            var expected =
                @"<copyright file=""NameOfFile.cs"" company=""CompanyName"">" + Environment.NewLine +
                @"     Company copyright tag." + Environment.NewLine +
                @"</copyright>";

            CommentFormatHelper.AssertEqualAfterFormat(input, expected);
        }

        [TestMethod]
        [TestCategory("Formatting UnitTests")]
        public void HeaderFormattingTests_PreservesHyphenLinesWithoutXML()
        {
            var input =
                @"--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine +
                Environment.NewLine +
                @"Copyright (c) 2012 - 2013 . All rights reserved." + Environment.NewLine +
                Environment.NewLine +
                @"--------------------------------------------------------------------------------------------------------------------";

            CommentFormatHelper.AssertEqualAfterFormat(input);
        }

        [TestMethod]
        [TestCategory("Formatting UnitTests")]
        public void HeaderFormattingTests_PreservesHyphenLinesWithXML()
        {
            // Same as above, indenting without a prefix sneaks in an extra space.
            var input =
                @"-----------------------------------------------------------------------" + Environment.NewLine +
                @"<copyright file=""NameOfFile.cs"" company=""CompanyName"">" + Environment.NewLine +
                @"     Company copyright tag." + Environment.NewLine +
                @"</copyright>" + Environment.NewLine +
                @"-----------------------------------------------------------------------";

            CommentFormatHelper.AssertEqualAfterFormat(input);
        }
    }
}