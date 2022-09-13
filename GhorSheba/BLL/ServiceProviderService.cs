using AutoMapper;
using BEL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceProviderService
    {

        public static ServUserModel GetServUser(int id)
        {
            var s = DataAccessFactory.ServUserDataAccess().Get(id);
            var us = DataAccessFactory.UserDataAccess().Get(id);
            var serv = new ServUserModel()
            {
                fullname = us.fullname,
                password = us.password,
                phone = s.phone,
                id = s.id,
            };
            return serv;
        }
        public static void EditServiceProvider(ServUserModel u)
        {
            var sp = DataAccessFactory.ServiceProviderDataAccess().Get();
            var us = DataAccessFactory.UserDataAccess().Get();

            var temp = new UserModel();
            var temp1 = new ServiceProviderModel();

            var a = new User();
            var b = new ServiceProvider();

            foreach(var x in sp)
            {
                if(x.id==u.id)
                {
                    var y = DataAccessFactory.UserDataAccess().Get(x.user_id);
                    a = y;
                    b = x;
                    break;
                }
            }

            a.fullname = u.fullname;
            a.password = u.password;
            b.phone = u.phone;
            ServiceProviderDataAccessFactory.UserProDataAccess().Edit(a);

            ServiceProviderDataAccessFactory.ServiceProviderDataAccess().Edit(b);
        }

        public static Book_Bookingser GetBook_Bookingser(int id)
        {
            var b = new Booking();
            var bs = new Booking_Service();

            b = ServiceProviderDataAccessFactory.SerBookingDataAccess().Get(id);
            //bs = ServiceProviderDataAccessFactory.SerBooking_ServiceDataAccess().Get(id);

            var u = new Book_Bookingser();

            u.id = id;
            u.customer_id = b.customer_id;
            u.total_cost = b.total_cost;
            u.status = b.status;
            u.payment_status = b.payment_status;
            //u.booking_id = bs.booking_id;
            // u.serviceprovider_id = bs.serviceprovider_id;

            return u;
        }

        public static void EditBooking(Book_Bookingser u)
        {
            var sb = ServiceProviderDataAccessFactory.SerBookingDataAccess().Get();

            int b_id = 0;
            var b = new BookingModel();
            foreach (var x in sb)
            {
                if (x.customer_id == u.customer_id)
                {
                    var config1 = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingModel>());
                    var mapper1 = new Mapper(config1);
                    var data1 = mapper1.Map<BookingModel>(x);
                    b = data1;
                    break;
                }
            }


            b.customer_id = u.customer_id;
            b.status = u.status;
            b.payment_status = u.payment_status;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<BookingModel, Booking>());
            var mapper = new Mapper(config);
            var data = mapper.Map<Booking>(b);

            ServiceProviderDataAccessFactory.SerBookingDataAccess().Edit(data);
        }

        public static List<SerReviewModel> GetReview()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Review, SerReviewModel>());
            var mapper = new Mapper(config);
            var data = mapper.Map<List<SerReviewModel>>(ServiceProviderDataAccessFactory.SerReviewDataAccess().Get());

            return data;
        }

        public static List<AdminBookingDetailModel> WorkDetail(int id)
        {


            var bd = DataAccessFactory.BookingDetailDataAccess().Get();
            var r = new List<AdminBookingDetailModel>();
            foreach (var x in bd)
            {
                if (x.booking_id == id)
                {
                    var s = DataAccessFactory.ServiceDataAccess().Get(x.service_id);
                    var temp = new AdminBookingDetailModel()
                    {
                        name = s.name,
                        category = s.category
                    };
                    r.Add(temp);
                }
            }

            return r;
        }

        public static ServiceProviderBonusModel GetServiceProviderBonusModel(int id)
        {
            var serv = new ServiceProvider();
            var bon = new Bonu();
            var serbon = new ServiceProvider_Bonus();

            serv = ServiceProviderDataAccessFactory.ServiceProviderDataAccess().Get(id);
            bon = ServiceProviderDataAccessFactory.SerBonusDataAccess().Get(id);
            serbon = ServiceProviderDataAccessFactory.ServiceProvider_BonusDataAccess().Get(id);

            var u = new ServiceProviderBonusModel();

            u.id = id;
            //u.service_provider_id = serbon.service_provider_id;
            //u.bonus_id = serbon.bonus_id;
            u.bonus_amount = bon.bonus_amount;
            //u.phone = serv.phone;
            //u.work_status = serv.work_status;

            return u;


        }

        public static List<ServiceDetailModel> GetDetail(int id)
        {
            var r = new List<ServiceDetailModel>();
            var b = DataAccessFactory.BookingServiceDataAccess().Get();
            var cus = DataAccessFactory.CustomerUserDataAccess().Get();
            var sp = DataAccessFactory.ServUserDataAccess().Get(id);
            var temp = new List<ServiceDetailModel>();

            foreach (var x in b)
            {
                if (x.serviceprovider_id == sp.id)
                {
                    var booking = DataAccessFactory.BookingDataAccess().Get(x.booking_id);

                    foreach (var z in cus)
                    {
                        if (z.id == booking.customer_id)
                        {
                            var us = DataAccessFactory.UserDataAccess().Get(z.user_id);
                            var a = new ServiceDetailModel()
                            {
                                id = booking.id,
                                fullname = us.fullname,
                                phone = z.phone,
                                address = z.address
                            };
                            temp.Add(a);
                        }
                    }
                }
            }
            return temp;
        }

        public static ServiceProviderModel GetServiceProviderModel(int id)
        {

            var serv = new ServiceProvider();

            serv = ServiceProviderDataAccessFactory.ServiceProviderDataAccess().Get(id);

            var u = new ServiceProviderModel();

            u.id = id;
            //u.phone = serv.phone;
            //u.rating = serv.rating;
            // u.rating_count = serv.rating_count;
            //u.services_done = serv.services_done;
            u.work_status = serv.work_status;

            return u;
        }

        public static void WorkStatus(ServiceProviderModel u)
        {
            var sp = DataAccessFactory.ServiceProviderDataAccess().Get(u.id);
            sp.work_status = u.work_status;
            ServiceProviderDataAccessFactory.ServiceProviderDataAccess().Edit(sp);
        }


        public static List<BookingModel> ViewBooking(int id)
        {
            var bs = DataAccessFactory.BookingServiceDataAccess().Get();
            var us = DataAccessFactory.ServUserDataAccess().Get(id);

            var temp = new List<BookingModel>();
            foreach (var x in bs)
            {
                if (x.serviceprovider_id == us.id)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingModel>());
                    var mapper = new Mapper(config);
                    var data = mapper.Map<BookingModel>(DataAccessFactory.BookingDataAccess().Get(x.booking_id));

                    temp.Add(data);

                }
            }
            return temp;
        }

        public static UserSalariesModel GetSalary(int id)
        {
            var sp = DataAccessFactory.SalariesDataAccess().Get(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Salary, SalariesModel>());
            var mapper = new Mapper(config);
            var data = mapper.Map<SalariesModel>(sp);

            DateTime dt = data.updated_at.AddDays(30);
            var u = new UserSalariesModel()
            {
                Due = (int)(dt - DateTime.Now.Date).TotalDays,
                message = data.message,
                salary_amount= data.salary_amount,
                last_paid= data.updated_at
            };
            return u;
        }

        public static BookingModel EditBooking(int id)
        {
            var b = DataAccessFactory.BookingDataAccess().Get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingModel>());
            var mapper = new Mapper(config);

            var data = mapper.Map<BookingModel>(b);
            return data;
        }

        public static void EditBooking(BookingModel b)
        {
            var u = DataAccessFactory.BookingDataAccess().Get(b.id);
            u.payment_status = b.payment_status;
            u.status = b.status;
            DataAccessFactory.BookingDataAccess().Edit(u);
        }

    }
}

