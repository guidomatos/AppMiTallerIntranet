using System;
using System.Collections;
using System.Collections.Generic;

public class JQGridJsonResponse
{
    public int CurrentPage = 1;
    public int RecordCount = 0;
    public List<JQGridJsonResponseRow> Items;
    public int PageCount = 0;
    public Hashtable userData = null;

    public JQGridJsonResponse(Int32 pageCount, Int32 currentPage, Int32 recordCount)
    {
        CurrentPage = currentPage;
        RecordCount = recordCount;
        PageCount = pageCount;
        Items = new List<JQGridJsonResponseRow>();
    }
}

public class JQGridJsonResponseRow
{
    public string ID;
    public object Row;
}