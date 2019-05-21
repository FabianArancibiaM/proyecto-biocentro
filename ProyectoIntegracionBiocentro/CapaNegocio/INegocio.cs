using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public interface INegocio<T>
    {
        bool crear(T t);
        T leer(T t);
        T leerId(int id);
        List<T> listar();
        bool actualizar(T t);
        int count();
    }
}
