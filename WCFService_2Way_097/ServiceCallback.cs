using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService_2Way_097
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceCallback : IServiceCallback
    {
        Dictionary<IClientCallback, string> userList = new Dictionary<IClientCallback, string>(); //Menyimpan data ketika online
        public void gabung(string username)
        {
            //untuk menampung user ketika baru daftar/buat akun
            IClientCallback koneksiGabung = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            userList[koneksiGabung] = username;
        }

        public void kirimPesan(string pesan)
        {
            //mengirim data user dan pesan ke user lain
            IClientCallback koneksiPesan = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            string user;
            if (!userList.TryGetValue(koneksiPesan, out user))
            {
                return;
            }
            foreach (IClientCallback other in userList.Keys)
            {
                other.pesanKirim(user, pesan);
            }
        }
    }
}