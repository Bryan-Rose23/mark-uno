﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class clsNegocioUsuario
    {
        public clsDatoUsuario cdUsuario = new clsDatoUsuario();
        public clsEntidadUsuario ceUsuario = new clsEntidadUsuario();

        public string ConvertirSHA256(string strCadena) 
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create()) 
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(strCadena));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
