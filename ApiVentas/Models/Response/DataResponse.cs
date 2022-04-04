namespace ApiVentas.Models.Response
{
    public class DataResponse
    {
        public int Success {get;set;}
        public String Messages {get;set;}
        public Object Data {get;set;}
        public DataResponse(){
            this.Success=0;
        }
    }
}