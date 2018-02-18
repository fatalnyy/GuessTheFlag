using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheFlag
{
    ///<Summary>
    /// Gets the answer
    ///</Summary>

    public class Question
    {
        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;
        private string _goodAnswer;
        private string _capitalCity;
        private string _imagePath;
        private string _tip;

        #region Properties
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string AnswerA
        {
            get
            {
                return _answerA;
            }
            set
            {
                _answerA = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string AnswerB
        {
            get
            {
                return _answerB;
            }
            set
            {
                _answerB = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string AnswerC
        {
            get
            {
                return _answerC;
            }
            set
            {
                _answerC = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string AnswerD
        {
            get
            {
                return _answerD;
            }
            set
            {
                _answerD = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string GoodAnswer
        {
            get
            {
                return _goodAnswer;
            }
            set
            {
                _goodAnswer = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string CapitalCity
        {
            get
            {
                return _capitalCity;
            }
            set
            {
                _capitalCity = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string Tip
        {
            get
            {
                return _tip;
            }
            set
            {
                _tip = value;
            }
        }
        #endregion


    }
}
