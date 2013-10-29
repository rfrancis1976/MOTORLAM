using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using inercya.ORMLite;
using Motorlam.Entities;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace Motorlam.Data
{
    public class DataService : DataAccess
    {
        public DataService() : base("Motorlam")
        {
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
                if (_user != null)
                {
                    this.CurrentUserId = _user.UserId;
                }
                else
                {
                    this.CurrentUserId = null;
                }
            }
        }

        public static DataService Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return (DataService)HttpContext.Current.Items["DataService"];
                }
                else
                {
                    return null;
                }
            }
        }

        #region REPOSITORY

        private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(this);
                }
                return _userRepository;
            }
        }

        private CustomerRepository _customerRepository;
        public CustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(this);
                }
                return _customerRepository;
            }
        }

        private BrandRepository _brandRepository;
        public BrandRepository BrandRepository
        {
            get
            {
                if (_brandRepository == null)
                {
                    _brandRepository = new BrandRepository(this);
                }
                return _brandRepository;
            }
        }

        private BrandProductRepository _brandProductRepository;
        public BrandProductRepository BrandProductRepository
        {
            get
            {
                if (_brandProductRepository == null)
                {
                    _brandProductRepository = new BrandProductRepository(this);
                }
                return _brandProductRepository;
            }
        }

        private CarRepository _carRepository;
        public CarRepository CarRepository
        {
            get
            {
                if (_carRepository == null)
                {
                    _carRepository = new CarRepository(this);
                }
                return _carRepository;
            }
        }

        private DeliveryRepository _deliveryRepository;
        public DeliveryRepository DeliveryRepository
        {
            get
            {
                if (_deliveryRepository == null)
                {
                    _deliveryRepository = new DeliveryRepository(this);
                }
                return _deliveryRepository;
            }
        }

        private DeliveryLineRepository _deliveryLineRepository;
        public DeliveryLineRepository DeliveryLineRepository
        {
            get
            {
                if (_deliveryLineRepository == null)
                {
                    _deliveryLineRepository = new DeliveryLineRepository(this);
                }
                return _deliveryLineRepository;
            }
        }

        private FileRepository _fileRepository;
        public FileRepository FileRepository
        {
            get
            {
                if (_fileRepository == null)
                {
                    _fileRepository = new FileRepository(this);
                }
                return _fileRepository;
            }
        }

        private InvoiceRepository _invoiceRepository;
        public InvoiceRepository InvoiceRepository
        {
            get
            {
                if (_invoiceRepository == null)
                {
                    _invoiceRepository = new InvoiceRepository(this);
                }
                return _invoiceRepository;
            }
        }

        private InvoiceLineRepository _invoiceLineRepository;
        public InvoiceLineRepository InvoiceLineRepository
        {
            get
            {
                if (_invoiceLineRepository == null)
                {
                    _invoiceLineRepository = new InvoiceLineRepository(this);
                }
                return _invoiceLineRepository;
            }
        }

        private IvaRepository _ivaRepository;
        public IvaRepository IvaRepository
        {
            get
            {
                if (_ivaRepository == null)
                {
                    _ivaRepository = new IvaRepository(this);
                }
                return _ivaRepository;
            }
        }

        private MethodPaymentRepository _methodPaymentRepository;
        public MethodPaymentRepository MethodPaymentRepository
        {
            get
            {
                if (_methodPaymentRepository == null)
                {
                    _methodPaymentRepository = new MethodPaymentRepository(this);
                }
                return _methodPaymentRepository;
            }
        }

        private ModelRepository _modelRepository;
        public ModelRepository ModelRepository
        {
            get
            {
                if (_modelRepository == null)
                {
                    _modelRepository = new ModelRepository(this);
                }
                return _modelRepository;
            }
        }

        private ModelMotorRepository _modelMotorRepository;
        public ModelMotorRepository ModelMotorRepository
        {
            get
            {
                if (_modelMotorRepository == null)
                {
                    _modelMotorRepository = new ModelMotorRepository(this);
                }
                return _modelMotorRepository;
            }
        }

        private MotorRepository _motorRepository;
        public MotorRepository MotorRepository
        {
            get
            {
                if (_motorRepository == null)
                {
                    _motorRepository = new MotorRepository(this);
                }
                return _motorRepository;
            }
        }

        private ProductRepository _productRepository;
        public ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(this);
                }
                return _productRepository;
            }
        }

        private ProvinceRepository _provinceRepository;
        public ProvinceRepository ProvinceRepository
        {
            get
            {
                if (_provinceRepository == null)
                {
                    _provinceRepository = new ProvinceRepository(this);
                }
                return _provinceRepository;
            }
        }

        private SupplierRepository _supplierRepository;
        public SupplierRepository SupplierRepository
        {
            get
            {
                if (_supplierRepository == null)
                {
                    _supplierRepository = new SupplierRepository(this);
                }
                return _supplierRepository;
            }
        }

        private TypeProductRepository _typeProductRepository;
        public TypeProductRepository TypeProductRepository
        {
            get
            {
                if (_typeProductRepository == null)
                {
                    _typeProductRepository = new TypeProductRepository(this);
                }
                return _typeProductRepository;
            }
        }

        private CityRepository _cityRepository;
        public CityRepository CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(this);
                }
                return _cityRepository;
            }
        }
        
        #endregion

        #region METHODS

        public override int Update(object entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Type entityType = entity.GetType();

            IQueryLite baseTableQuery = BaseTableQueryLite.Create(entityType);
            baseTableQuery.SetPropertyValue("DataAccess", this);
            object originalEntity = baseTableQuery.Get(entity.GetId());

            if (originalEntity == null){
                throw new InvalidOperationException(string.Format("Cannot update {0} with ID {1} because it doesn't exist", entityType.Name, entity.GetId()));
            }

            var getters = PropertyHelper.GetPropertyGetters(entityType);
            var metadata = inercya.ORMLite.DataAccess.GetEntityMetadata(entityType);

            var isModified = false;
            foreach (var propKeyVal in metadata.Properties)
            {
                var sqlField = propKeyVal.Value.SqlField;
                if (sqlField != null){
                    var fieldName = propKeyVal.Value.PropertyInfo.Name;

                    if (sqlField.BaseTableName == metadata.BaseTableName
                        && !sqlField.IsAutoincrement
                        && !sqlField.IsKey
                        && !sqlField.IsReadOnly
                        && fieldName != this.ModifiedDateFieldName
                        && fieldName != this.ModifiedByFieldName
                        && fieldName != this.CreatedDateFieldName
                        && fieldName != this.CreatedByFieldName
                        && fieldName != this.EntityRowVersionFieldName
                        && !object.Equals(getters[fieldName](entity), getters[fieldName](originalEntity))
                        )
                    {
                        isModified = true;
                        //break;
                    }
                }
            }

            if (!isModified) return 0;

            int affectedRows = 0;
            affectedRows = base.Update(entity);
            if (affectedRows == 0){
                throw new DBConcurrencyException("Updated cancelled. Concurrency conflict, the loaded data have been modified by another user");
            }

            return affectedRows;
        }

        #endregion

    }
}
