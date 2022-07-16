using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Slider
    {
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public string SliderType { get; set; }
        public int Sort { get; set; }
        public string Title { get; set; }
        public bool Show { get; set; }
        public long SliderCategoryId { get; set; }
        public SliderCategory SliderCategory { get; set; }
    }
}
