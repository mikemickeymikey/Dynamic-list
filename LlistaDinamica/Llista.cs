using System.Collections.Generic;

namespace LlistaDinamica
{
    public class Llista<T> : IList<T>
    {
        class Contenidor
        {
            T dada;
            Contenidor seguent;
            Contenidor anterior;

            public Contenidor()
            {
                Dada = default;
                Seguent = default;
                Anterior = default;
            }

            public T Dada { get => dada; set => dada = value; }
            public Llista<T>.Contenidor? Seguent { get => seguent; set => seguent = value; }
            public Llista<T>.Contenidor? Anterior { get => anterior; set => anterior = value; }

        }

        Contenidor centinellaInici;
        Contenidor centinellaFinal;
        int nElem;

        /// <summary>
        /// Get: Fem un recorregut de la llista per trobar el contenidor a l'index especificat i retornem la dada.
        /// Set: Insertem el valor en aquell index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="Exception"></exception>
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= nElem) throw new IndexOutOfRangeException(index + " FORA DE RANG");
                Contenidor temporal;
                temporal = centinellaInici;
                if (temporal == null) throw new Exception("la llista està buida.");
                for(int i = 0; i <= index; i++) temporal = temporal.Seguent;
                return temporal.Dada;
            }
            set
            {
                if (index < 0 || index >= nElem) throw new IndexOutOfRangeException(index + " FORA DE RANG");
                Insert((T)value, index);
            }
        }

        public Llista()
        {
            centinellaInici = new Contenidor();
            centinellaFinal = new Contenidor();
            centinellaInici.Seguent = centinellaFinal;
            centinellaInici.Anterior = null;
            centinellaFinal.Seguent = null;
            centinellaFinal.Anterior = centinellaInici;
        }

        public Llista(IEnumerable elements)
        {
            foreach (Contenidor element in elements)
            {
                Contenidor unElement = new Contenidor();
                unElement.Dada = element.Dada;
                unElement.Seguent = null;
                unElement.Anterior = centinellaFinal;
                centinellaFinal.Seguent = unElement;
                nElem++;
            }
        }

        /// <summary>
        /// Get: retorna nElem
        /// Set: assigna el valor a nElem
        /// </summary>
        public int Count
        {
            get { return nElem; }
            set { nElem = value; }
        }

        public bool IsReadOnly { get { return false; } }

        public bool IsFixedSize => false;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        T IList<T>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Afegeix un contenidor amb Dada dada al final de la cua.
        /// </summary>
        /// <param name="dada"></param>
        public void Add(T dada)
        {
            Contenidor unContenidor = new Contenidor();
            unContenidor.Dada = dada;
            unContenidor.Anterior = centinellaFinal;
            unContenidor.Seguent = null;
            centinellaFinal.Seguent = unContenidor;
        }

        /// <summary>
        /// Buida la llista
        /// </summary>
        public void Clear()
        {
            Contenidor unContenidor = new Contenidor();
            while (centinellaInici.Seguent != null)
            {
                unContenidor = centinellaInici;
                centinellaInici = centinellaInici.Seguent;
                unContenidor = null;
            }
        }

        /// <summary>
        /// Retorna cert si la llista conté dada, fals altrament.
        /// </summary>
        /// <param name="dada"></param>
        /// <returns>cert si la llista conté dada, fals altrament</returns>
        /// <exception cref="Exception">si la llista està buida</exception>
        public bool Contains(T dada)
        {
            if (centinellaInici == null) throw new Exception("La llista està buida.");
            else
            {
                Contenidor temporal = centinellaInici;
                for (int i = 0; i < Count; i++)
                {
                    if (temporal.Dada.Equals(dada))
                    {
                        Console.WriteLine("node  exists in the Linked list:" + dada);
                        return true;
                    }
                    temporal = temporal.Seguent;
                }
                return false;
            }
        }

        /// <summary>
        /// Copia les dades a partir de l'index especificat sense eliminar-ne de la llista.
        /// </summary>
        /// <param name="dades">array de dades</param>
        /// <param name="index">posicio per inserir</param>
        public void CopyTo(T[] dades, int index)
        {
            if (index < 0 || index >= nElem) throw new IndexOutOfRangeException(index + " FORA DE RANG");
            Contenidor temporal = new Contenidor();
            temporal = centinellaInici;
            if (temporal == null) throw new Exception("la llista està buida.");
            for (int i = 0; i <= index; i++) temporal = temporal.Seguent;
            foreach(T dada in dades)
            {
                Insert(dada, index);
                index++;
            }
        }

        /// <summary>
        /// Si troba la dada retorna l'index de la seva posició, retorna -1 altrament.
        /// </summary>
        /// <param name="dada">dada a cercar</param>
        /// <returns>index de la dada</returns>
        /// <exception cref="Exception">si la llista està buida</exception>
        public int IndexOf(T dada)
        {
            Contenidor temporal = new Contenidor();
            temporal.Dada = dada;
            temporal = centinellaInici;
            bool trobat = false;
            int i = 0;
            if (temporal == null) throw new Exception("la llista està buida.");
            while (temporal != null)
            {
                i++;
                if (temporal.Dada.Equals(dada))
                {
                    trobat = true;
                    break;
                }
                temporal = temporal.Seguent;
            }
            if (trobat) return i;
            else return -1;
        }

        /// <summary>
        /// Insereix la dada a l'index especificat sense eliminar ninguna de la llista.
        /// </summary>
        /// <param name="dada">dada a insertar</param>
        /// <param name="index">posició a insertar</param>
        /// <exception cref="Exception">si la llista està buida</exception>
        public void Insert(T dada, int index)
        {
            Contenidor insertat = new Contenidor();
            insertat.Dada = dada;
            if (centinellaInici == null) throw new Exception("La llista està buida.");
            else if (index == 0)
            {
                insertat.Seguent = centinellaInici;
                insertat.Anterior = null;
                centinellaInici.Anterior = insertat;
                centinellaFinal = insertat;
            }
            else if (index >= Count)
            {
                insertat.Seguent = null;
                centinellaFinal.Seguent = insertat;
                insertat.Anterior = centinellaFinal;
                centinellaFinal = insertat;
            }
        }

        /// <summary>
        /// Cerca l'index de la dada i elimina aquell contenidor.
        /// </summary>
        /// <param name="dada">dada a eliminar</param>
        /// <returns>cert si s'ha eliminat, excepció altrament</returns>
        /// <exception cref="Exception">si la llista no conté la dada o està buida</exception>
        public bool Remove(T dada)
        {
            int posicio = IndexOf(dada);
            if (posicio < 0 || posicio >= nElem) throw new IndexOutOfRangeException(dada + " NO ESTÀ A LA LLISTA");
            if (centinellaInici == null) throw new Exception("La llista està buida.");
            else if (posicio == 0)
            {
                if (Count == 1)
                {
                    centinellaInici = null;
                    centinellaFinal = null;
                    Count--;
                }
                else
                {
                    centinellaInici = centinellaInici.Seguent;
                    centinellaInici.Anterior = null;
                    Count--;
                }
            }
            else if (posicio >= Count)
            {
                Contenidor temporal = centinellaFinal.Anterior;
                if (temporal == centinellaInici)
                {
                    centinellaFinal = centinellaInici = null;
                    Count--;
                }
                temporal.Seguent = null;
                centinellaFinal = temporal;
            }
            else
            {
                Contenidor temporal = centinellaInici;
                for(int i = 0; i < posicio - 1; i++)
                {
                    temporal = temporal.Seguent;
                }
                temporal.Seguent = temporal.Seguent.Seguent;
                temporal.Seguent.Anterior = temporal;
                Count--;
            }
            return true;
        }

        /// <summary>
        /// Elimina el contenidor de la posició especificada.
        /// </summary>
        /// <param name="posicio">posició a eliminar</param>
        /// <exception cref="Exception">si la llista està buida</exception>
        public void RemoveAt(int posicio)
        {
            if (centinellaInici == null) throw new Exception("La llista està buida.");
            else if (posicio == 0)
            {
                if (Count == 1)
                {
                    centinellaInici = null;
                    centinellaFinal = null;
                    Count--;
                }
                else
                {
                    centinellaInici = centinellaInici.Seguent;
                    centinellaInici.Anterior = null;
                    Count--;
                }
            }
            else if (posicio >= Count)
            {
                Contenidor temporal = centinellaFinal.Anterior;
                if (temporal == centinellaInici)
                {
                    centinellaFinal = centinellaInici = null;
                    Count--;
                }
                temporal.Seguent = null;
                centinellaFinal = temporal;
            }
            else
            {
                Contenidor temporal = centinellaInici;
                for (int i = 0; i < posicio - 1; i++)
                {
                    temporal = temporal.Seguent;
                }
                temporal.Seguent = temporal.Seguent.Seguent;
                temporal.Seguent.Anterior = temporal;
                Count--;
            }

            
        }

        /// <summary>
        /// Yield return dels contenidors de la llista
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">si la llista està buida</exception>
        public IEnumerator<T> GetEnumerator()
        {
            if (centinellaInici == null) throw new Exception("La llista està buida.");
            Contenidor actual = centinellaInici;
            while (actual != null)
            {
                T result = actual.Dada;
                actual = actual.Seguent;

                yield return result;
            }
        }

        #region Funcions IList
        public int Add(object? value)
        {
            Add((T)value);
            return IndexOf(value);
        }

        public bool Contains(object? value)
        {
            return Contains(value);
        }

        public int IndexOf(object? value)
        {
            return IndexOf(value);
        }

        public void Insert(int index, object? value)
        {
            Insert(index, value);
        }

        public void Remove(object? value)
        {
            Remove(value);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
