using ApiProject2.Context;
using ApiProject2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http.Json;
using System.Windows;
using System.Diagnostics;

namespace ApiProject2.Controllers;

using System.IO;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
   
  private readonly da8k2ujd2nc9e6Context _da8K2Ujd2Nc9E6Context;
   private readonly CrmContext _CrmContext;

    
    public ProductController(da8k2ujd2nc9e6Context da8K2Ujd2Nc9E6Context,CrmContext crmContext )
    {
        this._da8K2Ujd2Nc9E6Context = da8K2Ujd2Nc9E6Context;
        this._CrmContext = crmContext;
       
    }

 
     [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var empleado = this._da8K2Ujd2Nc9E6Context.Equifaxes.ToList();
        return Ok(empleado);
    }

     [HttpGet("getdoc/{DNI}/{RUC}/{RUC20}Buen pagador")]
        public async Task<IActionResult> GetBuenPagador(string DNI,string RUC,string RUC20)
        {
            if (DNI == "0" && RUC20 == "0") 
            {
                var equi = await _da8K2Ujd2Nc9E6Context.Equifaxes.FirstOrDefaultAsync(m => m.Ruc == RUC);
                double dias= 1;
                if(equi != null)
                {
                    DateTime fecha = (DateTime)equi.FechaCreacionRuc;
                    DateTime fechanow = DateTime.Now;
                    dias = ((fechanow-fecha).TotalDays);
                }
                if (equi == null)
                {
                   // var equi1 = await GetSerConRuc(RUC);
                   // return (equi1);
                }
                else if(dias>=30)
                {
                   _da8K2Ujd2Nc9E6Context.Equifaxes.Remove(equi);
                    await _da8K2Ujd2Nc9E6Context.SaveChangesAsync();
                  //  var equi1 = await  GetAntigudad(DNI);
                    //return Ok(equi1);
                }
                    return Ok(equi);
          
            }else{
                return NotFound("Error fatal,recargar pagina"); 
            }
        }



     [HttpPost("Get/Consultar_antiguedad_RUC")]
    public async Task<IActionResult> GetAntigudadRUC(string ruc){
        
       string searchruc = ruc.Trim();
       List<string>lis = new  List<string>();
         try{
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {   
                string query = "sp_validar_antiguedadRC";
                Trace.Write(string.Format("Query is: {0}", query));
                using(SqlCommand SqlCmd = new SqlCommand(query,conn))
                {
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@ruc_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchruc;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
            });
         if(resultado == ""){
         
         lis.Add ("No se encontro el mensaje");

        }else{
          
  
         lis.Add (resultado);
        }
        
       
        var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
        // return Ok(lis);
      
         }catch
       {
          return BadRequest("Error");
       }
       
    

    }
     
    
   
   
   [HttpPost("Get/Consultar_Servicios_contratados_RUC")] 
   public async Task<IActionResult> validarSerConRuc(string ruc)
    {
       string searchruc = ruc.Trim();
       List<string>lis = new List<string>();

         try{
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {   
                string query = "sp_Validar_servicioscontratadosRUC";
                Trace.Write(string.Format("Query is: {0}", query));
                using(SqlCommand SqlCmd = new SqlCommand(query,conn))
                {;
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@ruc_entrante",System.Data.SqlDbType.NVarChar, 200).Value =searchruc;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
          });
        if(resultado == ""){
         
         lis.Add ("No se encontro el mensaje");

        }else{

         lis.Add (resultado);

        }
        
        var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
        // return Ok(lis);
        
       
            }catch
       {
          return BadRequest("Error");
       }


}

 [HttpPost("Get/Consultar_Servicios_contratados_DNI_o_CE")] 
public async Task<IActionResult> validarSerconDNIyCE(string dni,string ce){
   string searchdni = dni.Trim();   
   string searchce = ce.Trim();   
Object lis ;
   if(dni=="0"){

       lis = await validarSerconCE(searchce);
        
   }else if(ce == "0"){
       lis =await validarSerconDNI(searchdni);
      
          
   }else{
       return NotFound("Error fatal,recargar pagina"); 
   }
  return Ok(lis);
}

 protected async Task<IActionResult> validarSerconDNI(string dni)
    {
       string searchdni = dni.Trim();   
       List<string>lis = new List<string>();

         try{
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {
                string query = "sp_Validar_servicioscontratadosDNI";
                Trace.Write(string.Format("Query is: {0}", query));
                using(SqlCommand SqlCmd = new SqlCommand(query,conn))
                {
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@dni_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchdni;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
           
           });
            
        if(resultado == ""){
         
         lis.Add ("No se encontro el mensaje");

        }else{

         lis.Add (resultado);
        }
        
         
       
         var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
         //return Ok(lis);
       }catch
       {
          return BadRequest("Error");
       }

        
        

}
   protected async Task<IActionResult> validarSerconCE(string ce)
    {
       string searchce = ce.Trim(); 
       List<string>lis = new List<string>();
       
       try
       {
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {   
                string query = "sp_Validar_servicioscontratadosCE";
                Trace.Write(string.Format("Query is: {0}", query));
                using(SqlCommand SqlCmd = new SqlCommand(query,conn))
                { 
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@ce_entrante",System.Data.SqlDbType.NVarChar, 200).Value = ce;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
                
            }
       
          });
          if(resultado == ""){
         
         lis.Add ("No se encontro el mensaje");

        }else{

         lis.Add (resultado);
        }
         var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
         //return Ok(lis);
          }catch
       {
          return BadRequest("Error");
       }
    
    }
[HttpPost("Get/Consultar_distrito_Riesgo")] 

 public async Task<IActionResult> validarDistritodni(string distrito)
    {
       string searchdistrito = distrito.ToLower().Trim(); 
       
       List<string>lis = new List<string>();
       
        try
        {
         if(distrito != "0"){ 
    
         string resultado = "";
         
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {   
                string query = "sp_validar_distrito";
                Trace.Write(string.Format("Query is: {0}", query));
                using(SqlCommand SqlCmd = new SqlCommand(query,conn))
                {
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
              //  SqlCmd.Parameters.Add("@dni_entrante",System.Data.SqlDbType.NVarChar, 200).Value = dni;
                SqlCmd.Parameters.Add("@distrito",System.Data.SqlDbType.NVarChar, 200).Value = distrito;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
            });
        if(resultado == ""){
         
         lis.Add ("No se encontro el mensaje");

        }else if(resultado == "1"){

         lis.Add ("bajo riesgo");
        }else{

         lis.Add (resultado);

        }
        
         var strserialize = JsonConvert.SerializeObject (lis);
         return Ok(strserialize);
         }else
         {
                return NotFound("Error fatal,recargar pagina"); 
         }
         //return Ok(lis);
        }catch
       {
          return BadRequest("Error");
       }
    }


 [HttpPost("Get/Consultar_calcular_score_crediticio_DNI")] 
 public async Task<IActionResult> validarScoreDni(String dni)
    {
      string searchdni = dni.Trim(); 
       
       List<string>lis = new List<string>();
       try
        {
       
      
         string resultado = "";
          await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {
                string query = "sp_validar_scoredni";
                Trace.Write(string.Format("Query is: {0}", query));
                using (SqlCommand SqlCmd = new SqlCommand(query,conn))
                {
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@dni_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchdni;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }
                
                }
            }
             });
           if(resultado == ""){
         
           lis.Add ("No se encontro el mensaje");

           }else{

         lis.Add (resultado);
        }
    

         var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
         //return Ok(lis);
         }catch
       {
          return BadRequest("Error");
       }
    }

[HttpPost("Get/consultar_calcular_score_crediticio_CE")] 
 public async Task<IActionResult> validarScoreCE(String ce)
    {
      
       string searchce = ce.Trim();
           
       List<string>lis = new List<string>();
       try
        {
       
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {
                string query = "sp_validar_scoreCE";
                Trace.Write(string.Format("Query is: {0}", query));
                using (SqlCommand SqlCmd = new SqlCommand(query,conn))
                {
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@ce_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchce;
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
            });
           if(resultado == ""){
         
           lis.Add ("No se encontro el mensaje");

           }else{

         lis.Add (resultado);
        }
    
          var strserialize = JsonConvert.SerializeObject (lis);
        return Ok(strserialize);
         //return Ok(lis);
         }catch
       {
          return BadRequest("Error");
       }
    }


    [HttpPost("Get/consultar_calcular_score_crediticio_RUC_Y_RUC20")] 
    public async Task<IActionResult> validarScoreRuc(String Ruc,String RUC20)
    {
        string searchRuc = Ruc.Trim();
        string searchRUC20 = RUC20.Trim();
        List<string>lis = new List<string>();
        try
         {
      
         string doc = "";
         string resultado = "";
         await Task.Run(() =>
         {
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {   
                string query = "sp_validar_scoredRUC";
                Trace.Write(string.Format("Query is: {0}", query));
                using (SqlCommand SqlCmd = new SqlCommand(query,conn))
                { 
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
                
                if(Ruc =="0" && RUC20!="0"){

                       SqlCmd.Parameters.Add("@ruc_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchRUC20;
                       doc = searchRUC20;
                      
                }else if(Ruc !="0" && RUC20=="0"){
                       SqlCmd.Parameters.Add("@ruc_entrante",System.Data.SqlDbType.NVarChar, 200).Value = searchRuc;
                       doc = searchRuc;
                     
                }
                
                try
                {
                    conn.Open();
                    resultado = (string)SqlCmd.ExecuteScalar();
                    
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                }
            }
             });
         if(resultado == ""){
         
           lis.Add ("No se encontro el mensaje");

           }else{

          lis.Add (resultado);
           }
         
          var strserialize = JsonConvert.SerializeObject (lis);
          return Ok(strserialize);
         // return Ok(lis);
          }catch
       {
          return BadRequest("Error");
       }
    
    }
 // [HttpPost("calcular mensaje RUC ")] 
    private IActionResult GetScoreRuc20YRuc10(int detonador  ){
        List<string> lis = new List<string>(); 

        if(detonador == 0){
          
          lis.Add("Introdusca un valor valido");

        }else{
       
     // var  emple = _da8K2Ujd2Nc9E6Context.Equifaxes.ToList();  
         string resultado = "";
         using (SqlConnection conn = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection())
            {
                SqlCommand SqlCmd = new SqlCommand("devolver_mensaje",conn);
                SqlConnection sqlcon = new SqlConnection();
                SqlCmd.CommandType = CommandType.StoredProcedure;
            
                SqlCmd.Parameters.Add("@indicador",System.Data.SqlDbType.NVarChar, 200).Value = detonador;
                      
                try
                {
                    conn.Open();

                    resultado = (string)SqlCmd.ExecuteScalar();
                      
                }
                catch (Exception ex)
                {
                   ex.GetBaseException();
                }

                
            }
            if(resultado == ""){
               lis.Add("No se encontro el mensaje");
            }else{
               lis.Add(resultado.ToString());
         }
        }
       var strserialize = JsonConvert.SerializeObject (lis);

    return Ok(strserialize);
    }



   


 


 
[HttpPost("consultar Buen pagador RUC")] 
public IActionResult GetBuenPagadorRuc(string ruc){
    List<Equifax> emple = new List<Equifax>();
       
       
       SqlConnection conexion = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection();
      
    
       var comm = new SqlCommand("select * from EquifaxMed where Ruc = " + ruc);
     
       SqlDataAdapter reader = new SqlDataAdapter(comm);
      
       DataSet ds = new DataSet();
       reader.Fill(ds);
     
       string json = JsonConvert.SerializeObject(ds, Formatting.Indented);
          
        
        conexion.Close();
       
        
         return Ok(json);
        
}
[HttpPost("consultar Buen pagador DNI y CE")] 
public IActionResult GetBuenPagadorDni(string dni){
    List<Equifax> emple = new List<Equifax>();
       
       
       SqlConnection conexion = (SqlConnection)_da8K2Ujd2Nc9E6Context.Database.GetDbConnection();
      
    
       SqlCommand comando = new SqlCommand("sp_BuenPagadorDNI", conexion);
       comando.CommandType = System.Data.CommandType.StoredProcedure;
       comando.Parameters.Add("@dni_entrante",System.Data.SqlDbType.NVarChar, 200).Value = dni;
     
       SqlDataAdapter reader = new SqlDataAdapter(comando);
      
       DataSet ds = new DataSet();
       reader.Fill(ds);
     
       string json = JsonConvert.SerializeObject(ds, Formatting.Indented);
          
          

       conexion.Close();
       
        
       return Ok(json);
        
}
}