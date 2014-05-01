using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject
{
    public class ConstValue
    {
        public const int BASE_POINT = 400;
        public const int HEAD_POINT = -1000;
        public const int TAIL_POINT = 3000;
        public const double INGREDIENT_END_POINTX=150;
        public const double INGREDIENT_END_POINTY = 200;
        public const double INGREDIENT_IMAGE_END_POINTX = 750;
        public const double INGREDIENT_IMAGE_END_POINTY = 200;
        public const double INGREDIENT_SHOP_END_POINTX = 1000;
        public const double INGREDINET_SHOP_END_POINTY = 200;
        public static TimeSpan DRINK_LIST_SPREAD_SPEED = TimeSpan.FromMilliseconds(200);
        public static TimeSpan INGREDIENT_MOVE_SPEED = TimeSpan.FromMilliseconds(200);
        public static TimeSpan INGREDIENT_SHOP_CAR_SPEED = TimeSpan.FromMilliseconds(200);
    }
}
