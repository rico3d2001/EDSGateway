﻿using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    public record Email : ValueObject
    {
        private Email()
        {
            
        }
        public Email(string email)
        {
            Texto = email;
        }

        

        public string Texto { get; private set; }
    }
}


