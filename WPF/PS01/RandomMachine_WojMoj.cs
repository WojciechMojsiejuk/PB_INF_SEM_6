using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class RandomMachine
    {
        private List<string> coupons;

        public RandomMachine()
        {
            coupons = new List<string>();
        }

        public bool CouponsAvailable()
        {
            return coupons.Any();
        }

        public void AddCoupon(string coupon)
        {
            coupons.Add(coupon);
        }

        public string GetCoupon()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, coupons.Count);
            try
            {
                string result = coupons[index];
                coupons.RemoveAt(index);
                return result;
            }
            catch(ArgumentOutOfRangeException e)
            {
                if (e.Source != null)
                    Console.WriteLine("ArgumentOutOfRangeException: {0}", e.Source);
                throw;
            }
        }

        public List<string> GetAllCoupons()
        {
            return coupons;
        }
    }
}
