using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SiMPAC.Models;

namespace SiMPAC.Data
{
    interface IDataAccessObject<T>
    {
        T insert(T input);

        bool remove(int key);

        T search(int key);

        bool update(int key, T value);

        List<T> getAll();
    }
}
