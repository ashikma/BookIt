

namespace BookItWebService
{
    public class ModelFactory
    {
        private AppointmentCreator appointmentCreator;
        private ProductCreator productCreator;
        private ShoppingBasketCreator shoppingBasketCreator;
        private TreatmentCreator treatmentCreator;
        private UserCreator userCreator;
        private WorkCreator workCreator;


        public AppointmentCreator AppointmentCreator
        {
            get
            {
                if(this.appointmentCreator == null)
                    this.appointmentCreator = new AppointmentCreator();
                return this.appointmentCreator;
            }
        }

        public ProductCreator ProductCreator
        {
            get
            {
                if (this.productCreator == null)
                    this.productCreator = new ProductCreator();
                return this.productCreator;
            }
        }

        public ShoppingBasketCreator ShoppingBasketCreator
        {
            get 
            { 
                if(this.shoppingBasketCreator == null) 
                    this.shoppingBasketCreator = new ShoppingBasketCreator();
                return this.shoppingBasketCreator;
            }
        }

        public TreatmentCreator TreatmentCreator
        {
            get
            {
                if(this.treatmentCreator == null)
                    this.treatmentCreator = new TreatmentCreator();
                return this.treatmentCreator;

            }
        }

        public UserCreator UserCreator
        {
            get
            {
                if( this.userCreator == null)
                    this.userCreator = new UserCreator();
                return this.userCreator;
            }
        }

        public WorkCreator WorkCreator
        {
            get
            {
                if(this.workCreator == null)
                    this.workCreator = new WorkCreator();  
                return this.workCreator;
            }
        }

    }
}
