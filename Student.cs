using System;

namespace MockPracticalExam
{
    abstract class Student
    {
        const char UNDERGRADUATE = 'U';
        const char GRADUATE = 'G';
        const char UNKNOWN = 'X';
        const int CREDIT_LIMIT = 21;

        protected const string NOT_APPLICABLE = "N/A";
        protected const string NEW_YORK = "NY";
        protected const int TWELEVE_CREDITS = 12;

        char type;
        public char Type 
        { 
            get
            {
                return type;
            }  
            set
            {
                if(value.ToString() == string.Empty)
                    throw new InvalidInputException("Cannot be empty");

                type = value;
            } 
        }
        string name;
        public string Name
        { 
            get
            {
                return name;
            }  
            set
            {
                if(value == string.Empty)
                    throw new InvalidInputException("Cannot be empty");

                name = value;
            } 
        }
        string stateResidence;
        public string StateResidence 
        { 
            get
            {
                return stateResidence;
            }  
            set
            {
                if(value == string.Empty)
                    throw new InvalidInputException("Cannot be empty");

                stateResidence = value;
            } 
        }
        int credits;
        public int Credits 
        { 
            get
            {
                return credits;
            }  
            set
            {
                if(value >= CREDIT_LIMIT)
                    throw new InvalidInputException("Exceed over 21");
                else if(value < 0)
                    throw new InvalidInputException("Cannot have negative digits");

                credits = value;
            } 
        }

        public Student()
        {
            Type = UNKNOWN;
            StateResidence = NOT_APPLICABLE;
            Credits = 0;
        }

        public Student(char type, string name, string stateResidence, int credits)
        {
            Type = type;
            Name = name;
            StateResidence = stateResidence;
            Credits = credits;
        }


        public abstract double CalcResidenceDiscount();

        public override string ToString()
        {
            return $"Name: {Name}\nDegree Level: {(Type == UNDERGRADUATE ? "Undergraduate" : (Type == GRADUATE ? "Graduate" : "Unknown"))}"
                + $"\nState Residence: {StateResidence}\nNumber of Credits: {Credits}";
        }


    }
}
