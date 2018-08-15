using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace armoxiDB
{
    public class aramoxi
    {
        String[] Filtro;
        String[] Relacion;
        String activo;
        int registro;
        String[] Archivo;
        String[] Campos;
        String rutaT;

        public int OpenDB(String BD)
        {
            rutaT = BD;

            try
            {
                Archivo = Unkrypto(File.ReadAllText(BD + ".axdb"));
                CargaInfo();
                return 2;
            }
            catch{

                return 1;

            }
        }

        private void CargaInfo()
        {

            Campos = getindex();



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


        public String OxiField(int num)
        {


            String[] aux;
            int inicio;
            String salida;
            inicio = 0;

            

            for(int index = 0 ; index < num ; index ++)
            {

                aux = Campos[index].Split(Convert.ToChar(","));

                inicio += Convert.ToInt16(aux[0]);



            }

            aux = Campos[num].Split(Convert.ToChar(","));

            salida = activo.Substring(inicio, Convert.ToInt16(aux[2])); 

            return salida;

        }

        public void addData(int key,String data)
        {

            int index = 0;
            String[] aux;

            for(index = 0;index <= Campos.Length; index++)
            {

                aux = Campos[index].Split(Convert.ToChar(","));

                if (aux[0] == Convert.ToString(key))
                {

                    break;

                }


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

            Filtro[registro] = "";

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



        public void CloseDB()
        {

            Array.Clear(Filtro, 0, Filtro.Length);
            activo = "";
            registro = 0;
            Array.Clear(Campos ,0, Campos.Length);
            Array.Clear(Archivo,0, Archivo.Length);
            rutaT = "";

        }

        public void DeleteDB()
        {
            try
            {
                File.Delete(rutaT + ".axid");
                File.Delete(rutaT + ".axdb");

            }
            catch
            {

                MessageBox.Show("Error al eliminar");

                //messa( "error al eliminar");

            }

        }

        public void movefirst()
        {

            if (Filtro.Length > 0)
            {

                activo = Filtro[0];

                registro = 0;

            }
            else
            {

                Filtro = Archivo;

                activo = Filtro[0];

                registro = 0;


            }

        }


        public void MoveLastest()
        {


            if (Filtro.Length > 0)
            {

                activo = Filtro[Filtro.Length-1];

                registro = Filtro.Length -1;

            }
            else
            {

                Filtro = Archivo;

                activo = Filtro[Filtro.Length - 1];

                registro = Filtro.Length - 1;


            }


        }

        public void MoveNext()
        {

            if (registro == Filtro.Length)
            {

                MessageBox.Show("Se encuentra en el final del archivo");

            }
            else
            {

                activo = Filtro[registro + 1];
                registro += 1;

            }

        }

        public void MoveLast()
        {

            if (registro == 0)
            {

                MessageBox.Show("Se encuentra en el inicio del archivo");

            }
            else
            {

                activo = Filtro[registro - 1];
                registro -= 1;

            }

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

        private String[] Unkrypto(String Input)
        {
            String[] Output;
            String[] aux;
            String temp;
            char separador;
            char retorno;
            long index;
            long index2;
            index = 0;
            index2 = 0;
            separador = Convert.ToChar("|");
            retorno = Convert.ToChar("€");


            temp = "";

            if (Input.Length == 0){

                Output = "".Split(Convert.ToChar("4"));

            }  else
            {

                Output = Input.Split(retorno);

                for (index = 0;index <= Output.LongLength ; index ++)
                {

                    aux = Output[index].Split(separador);

                    for (index2 = 0; index2 <= aux.LongLength; index2 ++)
                    {

                        

                            temp = temp + Convert.ToString(char.ConvertFromUtf32(Convert.ToChar(Convert.ToDecimal(aux[index]) / 8)));

                        

                        //Convert.ToDecimal(aux[index]) / 8;

                    }

                    Output[index] = temp;

                }

                //aux = Input.Split( separador );
                //for (index = 0; index <= aux.LongLength; index ++ ){

                //    if (aux[index] == "€"){



                //    }else{

                //        Output = Output + Convert.ToString(char.ConvertFromUtf32(Convert.ToChar(Convert.ToDecimal(aux[index]) / 8)));

                //    }

                //    //Convert.ToDecimal(aux[index]) / 8;

                //}

            }

           

            return Output;
        }

    }
}
