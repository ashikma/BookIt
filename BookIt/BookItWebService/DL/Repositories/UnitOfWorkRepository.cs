namespace BookItWebService
{
    public class UnitOfWorkRepository
    {
        UserRepository userRepository;
        WorkRepository workRepository;
        TreatmentRepository treatmentRepository;
        ShoppingBasketRepository shoppingBasketRepository;
        ProductRepository productRepository;
        AppointmentRepository appointmentRepository;

        DbContext dbContext;

        public UnitOfWorkRepository(DbContext db)
        {
            this.dbContext = db;
        }



        //properties
        public UserRepository UserRepository
        {
            get
            {
                if(this.userRepository == null)
                    this.userRepository = new UserRepository(DbContext.GetInstance());
                return this.userRepository;
            }
        }

        public WorkRepository WorkRepository
        {
            get
            {
                if (this.workRepository == null)
                    this.workRepository = new WorkRepository(DbContext.GetInstance());
                return this.workRepository;
            }
        }

        public TreatmentRepository TreatmentRepository
        {
            get
            {
                if( this.treatmentRepository == null)
                    this.treatmentRepository = new TreatmentRepository(DbContext.GetInstance());
            return this.treatmentRepository;
            }
        }

        public ShoppingBasketRepository ShoppingBasketRepository
        {
            get
            {
                if(this.shoppingBasketRepository == null)
                    this.shoppingBasketRepository = new ShoppingBasketRepository(DbContext.GetInstance());
                return this.shoppingBasketRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if(this.productRepository == null)
                    this.productRepository = new ProductRepository(DbContext.GetInstance());
                return this.productRepository;
            }
        }

        public AppointmentRepository AppointmentRepository
        {
            get
            {
                if(this.appointmentRepository == null)
                    this.appointmentRepository = new AppointmentRepository(DbContext.GetInstance());    
                return this.appointmentRepository;
            }
        }
    }
}
