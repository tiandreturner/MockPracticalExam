using System;

namespace MockPracticalExam
{
    class Undergraduate : Student, IStudentTuition
    {
        const int UNDERGRADUATE_TUITION_RATE = 800;
        const int STANDARD_TUITION_RATE = 7500;
        const double UNDERGRADUATE_TUITION_DISCOUNT_RATE = 0.25;
        int scholarshipAmount;
        public int ScholarshipAmount 
        { 
            get
            {
                return scholarshipAmount;
            }  
            set
            {
                if(value < 0)
                    throw new InvalidInputException("Cannot have negative digits");

                scholarshipAmount = value;
            } 
        }
 
        public Undergraduate() : base()
        {
            ScholarshipAmount = 0;
        }

        public Undergraduate(char type, string name, string stateResidence, int credits, int scholarshipAmount) : base(type, name, stateResidence, credits)
        {
            ScholarshipAmount = scholarshipAmount;
        }

        public override double CalcResidenceDiscount()
        {
            return StateResidence == NEW_YORK ? CalcTuition() * UNDERGRADUATE_TUITION_DISCOUNT_RATE : 0;
        }

        double CalcTuition()
        {
            double tuition = Credits >= TWELEVE_CREDITS ? STANDARD_TUITION_RATE : UNDERGRADUATE_TUITION_RATE * Credits;
            double tuitionDiscount = CalcResidenceDiscount();
            return tuition - tuitionDiscount - ScholarshipAmount;
        }


        public override string ToString()
        {
            return base.ToString() + $"\nScholarship: {ScholarshipAmount:C}\nTuition: {CalcTuition():C}";
        }
    }
}
