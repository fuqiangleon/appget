﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using AppGet.Manifests;

namespace AppGet.CreatePackage.Parsers
{
    public static class VersionParser
    {
        private static readonly Regex[] VersionRegexes = {
            new Regex("((\\d+)\\.){2,4}\\d+"),
            new Regex("\\d{1,4}")

        };

        public static string Parse(Uri uri)
        {
            var source = uri.LocalPath + uri.Query;
            foreach (var regex in VersionRegexes)
            {
                var matches = regex.Matches(source);
                if (matches.Count > 0)
                {
                    return matches.Cast<Capture>()
                        .OrderByDescending(c => c.Value.Length)
                        .First().Value;
                }
            }

            return null;
        }

    }
}
