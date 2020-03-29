using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie1
{
    public class Machine
    {
        private List<string> coupons;
        private Random random;
        public Machine()
        {
            random = new Random();
            coupons = new List<string>();
        }
        public void addCoupon(string coupon)
        {
            coupons.Add(coupon);
        }
        public string getCoupon()
        {
            int num = random.Next(coupons.Count);
            string coupon = coupons[num];
            coupons.RemoveAt(num);
            return coupon;
        }
        public bool couponsState()
        {
            if(coupons.Count!=0) return true;
            return false;
        }
        public List<string> getCoupons()
        {
            return coupons;
        }
    }
}
