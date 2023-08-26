using EDSCore;
using Hub.Dominio.Agregado;
using Hub.Dominio.Interfaces;
using Hub.Dominio.ValueObjects;

namespace Hub.Dominio.Entities
{
    public record IdCustomer : AbsID
    {
        private IdCustomer()
        {
            
        }
        public IdCustomer(string id) : base(id)
        {
        }
    }

    public class Customer : Entidade<IdCustomer>
    {
        readonly IUnitOfWorkHub _unitOfWork;
       
        

        private Customer(IdCustomer id, IUnitOfWorkHub unitOfWork) : base(id)
        {
            _unitOfWork = unitOfWork;
        }

        

        public async Task IniciarCustomerComHub(IniciaCustomerProps props, HubAgregate hubAgregate)
        {
            Email = props.Email; Name = props.NomeUsuario; Foto = props.Foto;
            Status = props.Status;
            await _unitOfWork.HubRepositorio.Iniciar(hubAgregate);
        }

        public async Task Confirma(string idHub)
        {
           
            await _unitOfWork.HubRepositorio.Confirmar(idHub, this);
        }

        public CPF CPF { get; private set; } 
        public Email Email { get; private set; }
        public Nome Name { get; private set; }
        public WhatsApp WhatsApp { get; private set; }
        public Foto Foto { get; private set; }
        public StatusCustomerType Status { get; private set; }
        

       

        public void Confirmar(ConfirmaCustomerProps props)
        {
            Email = props.Email; Name = props.NomeUsuario; CPF = props.CPF; WhatsApp = props.Zap; Foto = props.Foto;
            Status = props.Status;
        }

        
    }



    



}


