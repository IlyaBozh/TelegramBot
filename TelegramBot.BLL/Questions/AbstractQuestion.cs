



namespace TelegramBot.BL.Questions
{
    public abstract class AbstractQuestion
    {
        public string Description { get; set; }
        public List<string> Variants { get; set; }
        public string TrueAnswer { get; set; }  
        public List<string> TrueAnswers { get; set; }
        public string UserAnswer { get; set; }
        public List<string> UserAnswers { get; set; }

       
        public AbstractQuestion()
        {

        }

        public abstract AbstractQuestion Clone();

        public abstract bool setAnswer(string massage);

        public void EditDiscription(string newDescription)
        {
            if(newDescription == " " )
            {
                throw new ArgumentNullException(nameof(newDescription));
            }
            Description = newDescription;
        }

        public void ChangeTrueAnswer(int index, string newTrueAnswer)
        {
            if(index < 0 || index > Variants.Count)
            {
                throw new IndexOutOfRangeException();
            }
            if(TrueAnswers is null)
            {
                throw new NullReferenceException(nameof(TrueAnswers));
            }
            else
            {
                TrueAnswers[index] = newTrueAnswer;
            }
        }
        public void ChangeTrueAnswer(string newTrueAnswer)
        {
            TrueAnswer = newTrueAnswer;
        }

        public void AddVariant(string newVariant)
        {
            if (newVariant == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                Variants.Add(newVariant);
            }
        }
        public void AddTrueAnswer(string newAnswer)
        {
            if (newAnswer == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                TrueAnswers.Add(newAnswer);
            }
        }
        public void RemoveVariant()
        {
            if(Variants.Count == 0)
            {
                throw new ArgumentNullException();
            }
            else
            {
                int lastIndex = Variants.Count - 1;
                Variants.RemoveAt(lastIndex);
            }
        }
        public void RemoveVariantByIndex(int index)
        {
            if(index < 0 || index > Variants.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else if(Variants.Count == 0)
            {
                throw new NullReferenceException();
            }
            else
            {
                Variants.RemoveAt(index);
            }
        }
        public void RemoveTrueAnswerByIndex(int index)
        {
            if (index < 0 || index > TrueAnswers.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else if (TrueAnswers.Count == 0)
            {
                throw new NullReferenceException();
            }
            else
            {
                TrueAnswers.RemoveAt(index);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is AbstractQuestion))
            {
                return false;
            }
            AbstractQuestion question = (AbstractQuestion)obj;
            if (question.Description != Description)
            {
                return false;
            }
            if (question.TrueAnswer != TrueAnswer)
            {
                return false;
            }
            if (question.UserAnswer != UserAnswer)
            {
                return false;
            }
            if (question.Variants is not null && question.Variants.Count != Variants.Count)
            {
                return false;
                for (int i = 0; i < Variants.Count; i++)
                {
                    if (Variants[i] != question.Variants[i])
                    {
                        return false;
                    }
                }
            }
            if (question.TrueAnswers is not null && question.TrueAnswers.Count != TrueAnswers.Count)
            {
                return false;
                for (int i = 0; i < TrueAnswers.Count; i++)
                {
                    if (TrueAnswers[i] != question.TrueAnswers[i])
                    {
                        return false;
                    }
                }
            }
            if (question.UserAnswers is not null && question.UserAnswers.Count != UserAnswers.Count)
            {
                return false;
                for (int i = 0; i < UserAnswers.Count; i++)
                {
                    if (UserAnswers[i] != question.UserAnswers[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override string ToString()
        {
            string tmp = $"[{Description}]";

            if(Variants != null)
            {
                for (int i = 0; i < Variants.Count; i++)
                {
                    tmp += $"[{Variants[i]}]";
                }
            }
            if(TrueAnswers != null)
            {
                for (int i = 0; i < TrueAnswers.Count; i++)
                {
                    tmp += $"[{TrueAnswers[i]}]";
                }
            }
            if(UserAnswers != null)
            {
                for (int i = 0; i < UserAnswers.Count; i++)
                {
                    tmp += $"[{UserAnswers[i]}]";
                }
            }
            return tmp;
        }
    }
}
