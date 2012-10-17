// Guids.cs
// MUST match guids.h
using System;

namespace SlickQA.TestFileEditor
{
    static class GuidList
    {
        public const string GUID_TEST_FILE_EDITOR_PKG_STRING = "03784121-cc93-4f52-8c79-ed979e1afdc6";
	    public const string GUID_TEST_FILE_EDITOR_FACTORY_STRING = "e69f42bf-c663-4154-9baf-e34962673d86";

	    public static readonly Guid GuidTestFileEditorEditorFactory = new Guid(GUID_TEST_FILE_EDITOR_FACTORY_STRING);
    };
}