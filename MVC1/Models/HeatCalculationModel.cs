using System;
using System.Collections.Generic;

namespace MVC1.Models
{
    public class HeatCalculationModel
    {
        public double H0 { get; set; } // Высота слоя
        public double DeltaH0 { get; set; } // Шаг высоты слоя
        public double Tm { get; set; } // Начальная температура материала
        public double Tg { get; set; } // Начальная температура газа
        public double Wg { get; set; } // Скорость газа
        public double Cg { get; set; } // Средняя теплоемкость газа
        public double Gm { get; set; } // Расход материала
        public double Cm { get; set; } // Теплоемкость материала
        public double AlphaV { get; set; } // Объемный коэффициент теплоотдачи
        public double D { get; set; } // Диаметр аппарата

        public double M => (Gm * Cm) / (Wg * Cg * Math.Pow(D / 2.0, 2) * Math.PI);
        public double Y0 => (AlphaV * H0) / (Wg * Cg * 1000);

        public List<CalculationResult> CalculateTable()
        {
            var results = new List<CalculationResult>();

            for (double y = 0; y <= H0; y += DeltaH0)
            {
                double Y = (AlphaV * y) / (Wg * Cg);
                double upsilon = (1 - Math.Exp((M - 1) * Y / M)) / (1 - M * Math.Exp((M - 1) * Y0 / M));
                double theta = (1 - M * Math.Exp((M - 1) * Y / M)) / (1 - M * Math.Exp((M - 1) * Y0 / M));
                int t = (int)Math.Round(Tm + (Tg - Tm) * upsilon);
                int T = (int)Math.Round(Tm + (Tg - Tm) * theta);
                double deltaT = Math.Abs(t - T);

                results.Add(new CalculationResult
                {
                    _y = y,
                    TemperatureMaterial = t,
                    TemperatureGas = T,
                    TemperatureDifference = deltaT
                });
            }

            return results;
        }
    }

    public class CalculationResult
    {
        public double _y { get; set; }
        public int TemperatureMaterial { get; set; }
        public int TemperatureGas { get; set; }
        public double TemperatureDifference { get; set; }
    }
}
