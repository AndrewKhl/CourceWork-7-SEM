using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    [Flags]
    public enum MsgPromptType
    {
        None = 0,
        Ok = 1,
        Cancel = 2,
        No = 4,
        Yes = 8
    }

    public enum MsgTypes
    {
        Info,
        Warning,
        Error,
        Question
    }

    public delegate MsgPromptType DisplayMessageDelegate(object sender, string message, string title, MsgTypes msgType, MsgPromptType answerVariants);
    /// <summary>
    /// Allow object ot be notified before closing
    /// </summary>
    public interface IMessagePresenter
    {
        DisplayMessageDelegate MessagePresenter { get; set; }
    }
}
