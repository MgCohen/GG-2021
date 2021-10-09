using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWriterAddedSignal
{
    public OnWriterAddedSignal(Writer writer)
    {
        Writer = writer;
    }

    public Writer Writer;
}
