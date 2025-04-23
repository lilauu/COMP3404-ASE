using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private List<MessageViewModel> messages = [
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        new MessageViewModel() { Message = "holy shit does this actually work? no way" },
        ];
    public List<MessageViewModel> Messages
    {
        get => messages;
        set
        {
            messages = value;
            OnPropertyChanged();
        }
    }

    public MainPageViewModel() : base()
    {

    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
