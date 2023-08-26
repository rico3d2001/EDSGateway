using EDSCore;
using Hub.Dominio.Entities;
using Hub.Dominio.ValueObjects;

namespace Hub.Dominio.Agregado
{

   

    public record IdHub
    {
        private IdHub()
        {
            
        }
        public IdHub(string id)
        {
            MongoGuid = id;
        }

        public string MongoGuid { get; private set; }
    }

    public sealed class HubAgregate : Entidade<IdHub>, IAggregateRoot
    {

        List<Customer> _customers = new();
       

        

        private HubAgregate(IdHub idHub):base(idHub) 
        {
            
        }
       
        
        
        public Email Email { get; set; }
        
        public List<Customer> Customers { get => _customers; set => _customers = value; }
       

        public void AddCustomer(Customer customer)
        {
            try
            {
                _customers.Add(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task AddOrganizacao(Organizacao organizacao)
        //{
            
        //}

        //public async Task AddContaBase(string tipo)//IdCustomer idCustomer, Conta conta)
        //{
            

        //    switch (tipo)
        //    {
        //        case "Free":
        //            conta.IniciaContaFree();
        //            break;
        //        case "Corporate":
        //            conta.IniciaContaCorporate();
        //            break;
        //        case "Standard":
        //            conta.IniciaContaStandard();
        //            break;
        //    }

        //    this.Customers.First().CriaContaBase(conta);

        //    await conta.CriaBase(this);
            

        //}

        public async Task ConfirmaHub(Customer customer)
        {
            
            _customers.Clear();
            _customers.Add(customer);
            await customer.Confirma(this.Id.MongoGuid);
        }

     
    }

  

    public class IniciaCustomerProps
    {
        public Email Email { get; set; }
        public Nome NomeUsuario { get; set; }
        public Foto Foto { get; set; }
        public StatusCustomerType Status { get; set; }

    }

    public class ConfirmaCustomerProps
    {
        public IdHub Id { get; set; }
        public Email Email { get; set; }
        public CPF CPF { get; set; }
        public Nome NomeUsuario { get; set; }
        public WhatsApp Zap { get; set; }
        public Foto Foto { get; set; }
        public StatusCustomerType Status { get; set; }
        public int LimiteProjetos { get; set; }
        public int LimiteColaboradores { get; set; }

    }
}
