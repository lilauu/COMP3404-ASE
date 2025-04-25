using COMP3404_Client.API;
using COMP3404_Shared.Models.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client.SaveLoad;

public class ServerSaveLoadManager : ISaveLoadManager
{
    public static ServerSaveLoadManager Instance { get; private set; }

    private DataManager m_apiHelper;

    public ServerSaveLoadManager(DataManager apiHelper)
    {
        if (Instance is null)
            Instance = this;
        else
            return;

        m_apiHelper = apiHelper;
    }

    Task<IEnumerable<Chat>> ISaveLoadManager.LoadChatsAsync()
    {
        throw new NotImplementedException();
    }

    void ISaveLoadManager.SaveChat(Chat chat)
    {
        throw new NotImplementedException();
    }
}
