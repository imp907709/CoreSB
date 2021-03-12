﻿using System;

namespace crmvcsb.Universal
{
    public interface ISerialization
    {
        string Serialize(Object input);
        Object DeSerialize(string input);

        string Serialize<T>(T item);
        T DeSerialize<T>(string input);
    }
}
