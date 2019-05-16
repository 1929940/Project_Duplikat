using System.Collections.Generic;

namespace AppLibrary
{
    internal class PersonComparer : IEqualityComparer<PersonModel>
    {
        public bool Equals(PersonModel x, PersonModel y)
        {
            if(x.Pesel == y.Pesel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(PersonModel obj)
        {
            return obj.Pesel.GetHashCode();
        }
    }
}
