﻿using EDSCore;

namespace Hub.Dominio.ValueObjects
{

    public record WhatsApp : ValueObject
    {

        private WhatsApp()
        {
            
        }

        public WhatsApp(string value)
        {
            Texto = value.Trim();
        }

    
        public string Texto { get; private set; }

        


    }
}
