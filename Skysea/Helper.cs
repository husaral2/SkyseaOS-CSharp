﻿using IL2CPU.API.Attribs;
using System;

namespace Skysea
{
    internal class Helper
    {
        //Splits the entry
        public static string[] SpecialSplit(string str)
        {
            string[] splitted = str.Split('"', StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length <= 1)
                return str.Split(' ');
            foreach (string s in splitted) {
                if (s.StartsWith(' ') || s.EndsWith(' '))
                    s.Remove(s.IndexOf(' '));
            }
            return splitted;
        }

        //Returns the full path for the file
        public static string FindPath(string str, string dir)
        {
            if (str.Split('\\')[0].EndsWith(':'))
                return str;
            return dir + "\\" + str;
        }

        public static byte[] ELFExecutable = new byte[]
        {
            0x7F, 0x45, 0x4C, 0x46, 0x01, 0x01, 0x01, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x03, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x58, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x34, 0x00, 0x00, 0x00, 0x00, 0x00, 0x28, 0x00,
            0x09, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x8B, 0x44, 0x24, 0x04, 0xA2, 0x00, 0x00, 0x00,
            0x00, 0xC3, 0x8D, 0xB6, 0x00, 0x00, 0x00, 0x00,
            0xC6, 0x05, 0x00, 0x00, 0x00, 0x00, 0x0F, 0xC3,
            0x90, 0x8D, 0xB4, 0x26, 0x00, 0x00, 0x00, 0x00,
            0x31, 0xC0, 0x8D, 0xB6, 0x00, 0x00, 0x00, 0x00,
            0x8B, 0x15, 0x00, 0x00, 0x00, 0x00, 0xC6, 0x04,
            0x02, 0x00, 0x83, 0xC0, 0x01, 0x3D, 0xA0, 0x0F,
            0x00, 0x00, 0x75, 0xEC, 0xC7, 0x05, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC7, 0x05,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0xC3, 0xEB, 0x0D, 0x90, 0x90, 0x90, 0x90, 0x90,
            0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
            0x56, 0x53, 0x31, 0xC0, 0x8D, 0x74, 0x26, 0x00,
            0x8D, 0x90, 0xA0, 0x00, 0x00, 0x00, 0x89, 0xD6,
            0xEB, 0x0C, 0x8D, 0xB6, 0x00, 0x00, 0x00, 0x00,
            0x8D, 0x90, 0xA0, 0x00, 0x00, 0x00, 0x8B, 0x0D,
            0x00, 0x00, 0x00, 0x00, 0x0F, 0xB6, 0x1C, 0x11,
            0x88, 0x1C, 0x01, 0x8B, 0x0D, 0x00, 0x00, 0x00,
            0x00, 0x0F, 0xB6, 0x9C, 0x01, 0xA1, 0x00, 0x00,
            0x00, 0x88, 0x5C, 0x01, 0x01, 0x8B, 0x0D, 0x00,
            0x00, 0x00, 0x00, 0xC6, 0x04, 0x11, 0x00, 0x8B,
            0x15, 0x00, 0x00, 0x00, 0x00, 0xC6, 0x84, 0x02,
            0xA1, 0x00, 0x00, 0x00, 0x00, 0x83, 0xC0, 0x02,
            0x39, 0xC6, 0x75, 0xBC, 0x81, 0xFE, 0xE0, 0x0B,
            0x00, 0x00, 0x89, 0xF0, 0x75, 0xA2, 0x5B, 0x5E,
            0xC3, 0x8D, 0xB4, 0x26, 0x00, 0x00, 0x00, 0x00,
            0x8B, 0x44, 0x24, 0x04, 0x3C, 0x0A, 0x74, 0x45,
            0x3C, 0x09, 0x0F, 0x84, 0xA8, 0x00, 0x00, 0x00,
            0x3C, 0x08, 0x74, 0x6C, 0x8B, 0x15, 0x00, 0x00,
            0x00, 0x00, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00,
            0x0F, 0xAF, 0x15, 0x00, 0x00, 0x00, 0x00, 0x03,
            0x15, 0x00, 0x00, 0x00, 0x00, 0x88, 0x04, 0x51,
            0x0F, 0xB6, 0x0D, 0x00, 0x00, 0x00, 0x00, 0xA1,
            0x00, 0x00, 0x00, 0x00, 0x88, 0x4C, 0x50, 0x01,
            0xA1, 0x00, 0x00, 0x00, 0x00, 0x83, 0xC0, 0x01,
            0x83, 0xF8, 0x4F, 0x7E, 0x2B, 0xA1, 0x00, 0x00,
            0x00, 0x00, 0xC7, 0x05, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x83, 0xC0, 0x01, 0x83,
            0xF8, 0x13, 0x7F, 0x0C, 0xA3, 0x00, 0x00, 0x00,
            0x00, 0xC3, 0x8D, 0xB6, 0x00, 0x00, 0x00, 0x00,
            0xE9, 0x1B, 0xFF, 0xFF, 0xFF, 0x8D, 0x76, 0x00,
            0xA3, 0x00, 0x00, 0x00, 0x00, 0xC3, 0x66, 0x90,
            0xA1, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x15, 0x00,
            0x00, 0x00, 0x00, 0x0F, 0xAF, 0x05, 0x00, 0x00,
            0x00, 0x00, 0x03, 0x05, 0x00, 0x00, 0x00, 0x00,
            0x83, 0xE8, 0x01, 0xC6, 0x04, 0x42, 0x20, 0x8B,
            0x15, 0x00, 0x00, 0x00, 0x00, 0xC6, 0x44, 0x42,
            0x01, 0x0F, 0x83, 0x2D, 0x00, 0x00, 0x00, 0x00,
            0x01, 0xC3, 0x8D, 0xB6, 0x00, 0x00, 0x00, 0x00,
            0x83, 0x05, 0x00, 0x00, 0x00, 0x00, 0x04, 0xC3,
            0x56, 0x53, 0xBE, 0x00, 0x00, 0x00, 0x00, 0x8B,
            0x5C, 0x24, 0x0C, 0x6A, 0x5B, 0xC6, 0x05, 0x00,
            0x00, 0x00, 0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x59, 0xC6, 0x05, 0x00, 0x00, 0x00, 0x00,
            0x0A, 0xB8, 0x4C, 0x00, 0x00, 0x00, 0x8D, 0x76,
            0x00, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC6, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x06, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0x6A, 0x5D, 0xC6, 0x05, 0x00, 0x00, 0x00,
            0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF, 0xFF, 0x0F,
            0xBE, 0x03, 0x59, 0x84, 0xC0, 0x74, 0x1A, 0x89,
            0xF6, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC3, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x03, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0xC7, 0x44, 0x24, 0x0C, 0x0A, 0x00, 0x00,
            0x00, 0x5B, 0x5E, 0xE9, 0xC0, 0xFE, 0xFF, 0xFF,
            0x56, 0x53, 0xBE, 0x04, 0x00, 0x00, 0x00, 0x8B,
            0x5C, 0x24, 0x0C, 0x6A, 0x5B, 0xC6, 0x05, 0x00,
            0x00, 0x00, 0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x59, 0xC6, 0x05, 0x00, 0x00, 0x00, 0x00,
            0x0E, 0xB8, 0x57, 0x00, 0x00, 0x00, 0x8D, 0x76,
            0x00, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC6, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x06, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0x6A, 0x5D, 0xC6, 0x05, 0x00, 0x00, 0x00,
            0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF, 0xFF, 0x0F,
            0xBE, 0x03, 0x59, 0x84, 0xC0, 0x74, 0x1A, 0x89,
            0xF6, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC3, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x03, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0xC7, 0x44, 0x24, 0x0C, 0x0A, 0x00, 0x00,
            0x00, 0x5B, 0x5E, 0xE9, 0x40, 0xFE, 0xFF, 0xFF,
            0x56, 0x53, 0xBE, 0x09, 0x00, 0x00, 0x00, 0x8B,
            0x5C, 0x24, 0x0C, 0x6A, 0x5B, 0xC6, 0x05, 0x00,
            0x00, 0x00, 0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x59, 0xC6, 0x05, 0x00, 0x00, 0x00, 0x00,
            0x0C, 0xB8, 0x45, 0x00, 0x00, 0x00, 0x8D, 0x76,
            0x00, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC6, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x06, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0x6A, 0x5D, 0xC6, 0x05, 0x00, 0x00, 0x00,
            0x00, 0x0F, 0xE8, 0xFC, 0xFF, 0xFF, 0xFF, 0x0F,
            0xBE, 0x03, 0x59, 0x84, 0xC0, 0x74, 0x1A, 0x89,
            0xF6, 0x8D, 0xBC, 0x27, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x83, 0xC3, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x03, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0xC7, 0x44, 0x24, 0x0C, 0x0A, 0x00, 0x00,
            0x00, 0x5B, 0x5E, 0xE9, 0xC0, 0xFD, 0xFF, 0xFF,
            0x53, 0x8B, 0x5C, 0x24, 0x08, 0x0F, 0xBE, 0x03,
            0x84, 0xC0, 0x74, 0x15, 0x8D, 0x74, 0x26, 0x00,
            0x50, 0x83, 0xC3, 0x01, 0xE8, 0xFC, 0xFF, 0xFF,
            0xFF, 0x0F, 0xBE, 0x03, 0x5A, 0x84, 0xC0, 0x75,
            0xEF, 0x5B, 0xC3, 0x4C, 0x6F, 0x67, 0x00, 0x57,
            0x61, 0x72, 0x6E, 0x00, 0x45, 0x72, 0x72, 0x6F,
            0x72, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00,
            0x19, 0x00, 0x00, 0x00, 0x50, 0x00, 0x00, 0x00,
            0x00, 0x80, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x05, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x04, 0x00, 0xF1, 0xFF, 0x08, 0x00, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x10, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x1E, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x05, 0x00, 0x24, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x04, 0x00, 0x2F, 0x00, 0x00, 0x00,
            0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x04, 0x00, 0x39, 0x00, 0x00, 0x00,
            0x10, 0x02, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x42, 0x00, 0x00, 0x00,
            0x10, 0x03, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x4B, 0x00, 0x00, 0x00,
            0x0C, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x04, 0x00, 0x4F, 0x00, 0x00, 0x00,
            0x20, 0x00, 0x00, 0x00, 0x31, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x59, 0x00, 0x00, 0x00,
            0x10, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x69, 0x00, 0x00, 0x00,
            0xD0, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x72, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x05, 0x00, 0x78, 0x00, 0x00, 0x00,
            0x90, 0x02, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x82, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x04, 0x00, 0x85, 0x00, 0x00, 0x00,
            0x60, 0x00, 0x00, 0x00, 0x69, 0x00, 0x00, 0x00,
            0x12, 0x00, 0x01, 0x00, 0x00, 0x74, 0x65, 0x73,
            0x74, 0x2E, 0x63, 0x00, 0x74, 0x74, 0x79, 0x5F,
            0x6C, 0x6F, 0x67, 0x00, 0x74, 0x74, 0x79, 0x5F,
            0x73, 0x65, 0x74, 0x5F, 0x63, 0x6F, 0x6C, 0x6F,
            0x72, 0x00, 0x4C, 0x46, 0x42, 0x5F, 0x58, 0x00,
            0x56, 0x47, 0x41, 0x5F, 0x48, 0x45, 0x49, 0x47,
            0x48, 0x54, 0x00, 0x56, 0x47, 0x41, 0x5F, 0x57,
            0x49, 0x44, 0x54, 0x48, 0x00, 0x74, 0x74, 0x79,
            0x5F, 0x77, 0x61, 0x72, 0x6E, 0x00, 0x74, 0x74,
            0x79, 0x5F, 0x70, 0x75, 0x74, 0x73, 0x00, 0x4C,
            0x46, 0x42, 0x00, 0x74, 0x74, 0x79, 0x5F, 0x63,
            0x6C, 0x65, 0x61, 0x72, 0x00, 0x74, 0x74, 0x79,
            0x5F, 0x72, 0x65, 0x73, 0x65, 0x74, 0x5F, 0x63,
            0x6F, 0x6C, 0x6F, 0x72, 0x00, 0x74, 0x74, 0x79,
            0x5F, 0x70, 0x75, 0x74, 0x63, 0x00, 0x4C, 0x46,
            0x42, 0x5F, 0x59, 0x00, 0x74, 0x74, 0x79, 0x5F,
            0x65, 0x72, 0x72, 0x6F, 0x72, 0x00, 0x42, 0x47,
            0x00, 0x74, 0x74, 0x79, 0x5F, 0x73, 0x63, 0x72,
            0x6F, 0x6C, 0x6C, 0x5F, 0x75, 0x70, 0x00, 0x00,
            0x05, 0x00, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x12, 0x00, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x2A, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x3E, 0x00, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x48, 0x00, 0x00, 0x00, 0x01, 0x11, 0x00, 0x00,
            0x80, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x8D, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x9F, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0xA9, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0xE6, 0x00, 0x00, 0x00, 0x01, 0x11, 0x00, 0x00,
            0xEC, 0x00, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0xF3, 0x00, 0x00, 0x00, 0x01, 0x0A, 0x00, 0x00,
            0xF9, 0x00, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x03, 0x01, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x08, 0x01, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x11, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x1E, 0x01, 0x00, 0x00, 0x01, 0x11, 0x00, 0x00,
            0x24, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x35, 0x01, 0x00, 0x00, 0x01, 0x11, 0x00, 0x00,
            0x49, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x51, 0x01, 0x00, 0x00, 0x01, 0x11, 0x00, 0x00,
            0x57, 0x01, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x5E, 0x01, 0x00, 0x00, 0x01, 0x0A, 0x00, 0x00,
            0x64, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x71, 0x01, 0x00, 0x00, 0x01, 0x0D, 0x00, 0x00,
            0x7C, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x8A, 0x01, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00,
            0x93, 0x01, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00,
            0x9F, 0x01, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xA5, 0x01, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xAC, 0x01, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xC5, 0x01, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xD5, 0x01, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xDB, 0x01, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xF5, 0x01, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x13, 0x02, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00,
            0x1F, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x25, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x2C, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x45, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x55, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0x5B, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x75, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x93, 0x02, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00,
            0x9F, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xA5, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xAC, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xC5, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xD5, 0x02, 0x00, 0x00, 0x01, 0x13, 0x00, 0x00,
            0xDB, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0xF5, 0x02, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x25, 0x03, 0x00, 0x00, 0x02, 0x10, 0x00, 0x00,
            0x00, 0x2E, 0x73, 0x79, 0x6D, 0x74, 0x61, 0x62,
            0x00, 0x2E, 0x73, 0x74, 0x72, 0x74, 0x61, 0x62,
            0x00, 0x2E, 0x73, 0x68, 0x73, 0x74, 0x72, 0x74,
            0x61, 0x62, 0x00, 0x2E, 0x72, 0x65, 0x6C, 0x2E,
            0x74, 0x65, 0x78, 0x74, 0x00, 0x2E, 0x72, 0x6F,
            0x64, 0x61, 0x74, 0x61, 0x2E, 0x73, 0x74, 0x72,
            0x31, 0x2E, 0x31, 0x00, 0x2E, 0x64, 0x61, 0x74,
            0x61, 0x00, 0x2E, 0x62, 0x73, 0x73, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x1F, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x40, 0x00, 0x00, 0x00, 0x33, 0x03, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x1B, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00,
            0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x78, 0x05, 0x00, 0x00, 0xA0, 0x01, 0x00, 0x00,
            0x07, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
            0x25, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x73, 0x03, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x34, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00,
            0x84, 0x03, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x3A, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
            0x03, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00,
            0x94, 0x03, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x11, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x18, 0x07, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x94, 0x03, 0x00, 0x00, 0x50, 0x01, 0x00, 0x00,
            0x08, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00,
            0x09, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0xE4, 0x04, 0x00, 0x00, 0x93, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

    }
}
