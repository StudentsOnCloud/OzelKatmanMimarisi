using System.Collections.Generic;

namespace CustomProject.Common
{
    public interface IORM<T> where T : class 
    {
        Result<List<T>> Select();
        Result<bool> Insert(T entity);
        Result<bool> Update(T entity);
        Result<bool> Delete(T entity);
    }
}
