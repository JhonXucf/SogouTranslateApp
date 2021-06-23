using System;
using System.IO;

namespace SogouTranslateApp.Helper
{
    public class IniHelper
    {
        static readonly string GameIni_GameIni_NAME = "Config";
        static readonly string Ini_Path = "\\Config\\Config.ini";

        public INIParser m_IniFile;

        private string m_szPath;

        public IniHelper()
        {
            CPath path = new CPath("");

            CPath.ToggleFullPath(ref path);

            m_szPath = path.str;
            m_szPath = string.Format("{0}{1}", m_szPath, Ini_Path);
            m_IniFile = new INIParser();
            Open();
        }

        public void Open()
        {
            if (!File.Exists(m_szPath))
            {
                return;
            }

#if IOS_VEST
            m_IniFile.Open(m_szPath,true);
#else
            m_IniFile.Open(m_szPath, false);
#endif
        }

        public void SetString(string szSection, string szName, string szValue)
        {
            if (m_IniFile == null)
            {
                return;
            }

            m_IniFile.WriteValue(szSection, szName, szValue);
        }

        public void SetInt(string szSection, string szName, int nValue)
        {
            if (m_IniFile == null)
            {
                return;
            }

            m_IniFile.WriteValue(szSection, szName, nValue.ToString());
        }

        public void SetString(string szName, string szValue)
        {
            if (m_IniFile == null)
            {
                return;
            }

            m_IniFile.WriteValue(GameIni_GameIni_NAME, szName, szValue);
        }

        public void SetInt(string szName, int nValue)
        {
            if (m_IniFile == null)
            {
                return;
            }

            m_IniFile.WriteValue(GameIni_GameIni_NAME, szName, nValue.ToString());
        }

        public int GetInt(string szName, int nDefault)
        {
            if (m_IniFile == null)
            {
                return nDefault;
            }

            if (!m_IniFile.IsKeyExists(GameIni_GameIni_NAME, szName))
            {
                //TRACE.ErrorLn(string.Format("GameIni :GetInt Value Not Exists!,Section={0},szName={1}", GameIni_GameIni_NAME, szName));
                return nDefault;
            }

            return m_IniFile.ReadValue(GameIni_GameIni_NAME, szName, nDefault);
        }

        public string GetString(string szName, string szDefaultVal)
        {
            if (m_IniFile == null)
            {
                return szDefaultVal;
            }

            if (!m_IniFile.IsKeyExists(GameIni_GameIni_NAME, szName))
            {
                //TRACE.ErrorLn(string.Format("GameIni :GetString Value Not Exists!,Section={0},szName={1}", GameIni_GameIni_NAME, szName));
                return szDefaultVal;
            }

            return m_IniFile.ReadValue(GameIni_GameIni_NAME, szName, szDefaultVal);
        }
    };
}