using System;

namespace MockPracticalExam
{
    class Graduate : Student, IStudentTuition
    {
        const int GRADUATE_TUITION_RATE = 1050;
        const int STANDARD_TUITION_RATE = 9000;
        const double GRADUATE_TUITION_DISCOUNT_RATE = 0.20;
        string almaMater;
        public string AlmaMater
        { 
            get
            {
                return almaMater;
            }  
            set
            {
                if(value == string.Empty)
                    throw new InvalidInputException("Cannot be empty");

                almaMater = value;
            } 
        }
        public Graduate() : base()
        {
            AlmaMater = NOT_APPLICABLE;
        }

        public Graduate(char type, string name, string stateResidence, int credits, string almaMater) : base(type, name, stateResidence, credits)
        {
            AlmaMater = almaMater;
        }

        public override double CalcResidenceDiscount()
        {
            return StateResidence == NEW_YORK ? CalcTuition() * GRADUATE_TUITION_DISCOUNT_RATE : 0;
        }

        double CalcTuition()
        {
            double tuition = Credits >= TWELEVE_CREDITS ? STANDARD_TUITION_RATE : GRADUATE_TUITION_RATE * Credits;
            double tuitionDiscount = CalcResidenceDiscount();
            return tuition - tuitionDiscount;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nTuition: {CalcTuition():C}\nAlma Mater: {AlmaMater}";
        }
    }
}
