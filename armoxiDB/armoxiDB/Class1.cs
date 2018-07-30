using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;



namespace armoxiDB
{
    public class aramoxi
    {
        String[] Filtro;
        String[] activo;
        int registro;
        String Archivo;
        String[] Campos;
        String rutaT;

        public int OpenDB(String BD)
        {
            rutaT = BD;

            try
            {
                Archivo = Unkrypto(File.ReadAllText(BD + ".axdb"));

                return 2;
            }
            catch{

                return 1;

            }
        }

        public int createindex(long Clave, String nombreCampo, long Longitud)
        {
            try
            {
                if (!File.Exists(rutaT + ".axid"))
                {

                    File.Create(rutaT + ".axid");

                }

                File.AppendAllText(rutaT + ".axid", Clave + "," + nombreCampo + "," + Longitud + "|");

            return 1;

            }catch
            {

                return 2;
            }
        }

        public int createBD(String ruta, String nombredb)
        {

            try { 

            if (File.Exists(ruta)){

                    return 1;

            }else
            {

                File.Create(ruta +  nombredb + ".axdb");

                    return 2;

            }

            } catch
            {

                return 3;
                
            }

            
        }

        public String[] getindex()
        {

            String[] Output;

            Output = Convert.ToString(File.ReadAllText(rutaT + ".axid")).Split(Convert.ToChar("|"));
            
            return Output;

        }

        public void delete()
        {

            activo[registro] = "";

        }

        public int deleteindex(String Clave)
        {
            String[] aux;
            String[] Claves = getindex();
            long index = 0;
            
            for (index =0 ; index <= Claves.LongLength;index++)
            {

                aux = Claves[index].Split(Convert.ToChar(","));
                if (aux[0] == Clave)
                {

                    Claves[index] = "";

                    break;

                } 
            }

            grabarindices(Claves);

            return 1;

        }

        private int grabarindices(string[] claves)
        {
            try{

                int index;

                for (index = 0; index <= claves.LongLength;index++)
                {

                    if( claves[index] != "")
                    {

                        File.AppendAllText(rutaT + ".axid", claves[index] + "|");

                    }

                }

                return 1;

            }
            catch
            {
                return 2;
            }
        }

        private String Krypto(String Input)
        {
            String Output;
            int size;
            int index;
            Output = "";
            size = Input.Length;

            for(index=0; index <= size; index++)
            {

                Output = Output + Convert.ToString(Convert.ToDecimal((int)Convert.ToChar(Input.Substring(index,1)))*8);

            }

            return Output;

        }

        private String Unkrypto(String Input)
        {
            String Output;
            String[] aux;
            char separador;
            long index;
            index = 0;
            separador = Convert.ToChar("|");


            Output = "";

            if (Input.Length == 0){

            }  else
            {

                aux = Input.Split( separador );
                for (index = 0; index <= aux.LongLength; index ++ ){

                    Output = Output + Convert.ToString(char.ConvertFromUtf32(Convert.ToChar(Convert.ToDecimal(aux[index]) / 8)));

                    //Convert.ToDecimal(aux[index]) / 8;

                }

            }

           

            return Output;
        }

    }
}
