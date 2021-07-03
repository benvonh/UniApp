using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniApp.Model
{
    public class Assessment
    {
        private string name;
        private int weight;
        private int hurdle;
        private int? mark;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Weight
        {
            get => weight;
            set
            {
                if (value > 100 || value < mark)
                    throw new ArgumentException("Weight % must be between 0 and 100 (or no less than the mark given)");
                weight = value;
            }
        }

        public int Hurdle
        {
            get => hurdle;
            set
            {
                if (value > 100 || value < 0)
                    throw new ArgumentException("Hurdle % must be between 0 and 100");
                hurdle = value;
            }
        }

        public int? Mark
        {
            get => mark;
            set
            {
                if (value != null && (value > 100 || value < 0))
                    throw new ArgumentException("Mark % must be between 0 and 100");
                mark = value;
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int MarkDefault => mark ?? 0;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsComplete => mark != null;
    }
}
