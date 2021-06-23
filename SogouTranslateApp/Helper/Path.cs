
using System;
using System.IO;

namespace SogouTranslateApp.Helper
{
    public delegate DFP_RESULT DirFileProc(bool isFile, string absolutePath, string relativePath, string name, object userdata);
    public delegate bool DIRPROC(string absoluteDir, string relativeDir, object userdata);
    public delegate bool FILEPROC(string absoluteDir, string relativeDir, object userdata);

    /// 目录浏览mask
    public enum BrowseDirMask
    {
        bdm_file = 0x1,
        bdm_dir = 0x2
    };

    public enum DFP_RESULT
    {
        DFP_ABORT = 0,
        DFP_SUCCESS = 1,
        DFP_IGNORE = 2
    };

    public class CPath
    {
        private string _String;
        private bool _isFile;
        static string s_workDir = string.Empty;

        public const string CHAR_SLASH = "\\";
        public const string CHAR_DOT = ".";
        public const string CHAR_SLASH2 = "/";

        public string str
        {
            set
            {
                _String = value;
            }
            get
            {
                return _String;
            }
        }

        // 构造函数
        public CPath()
        {
            _isFile = true;
        }

        public CPath(string p, bool isFilePath = true, int roff = 0, int count = 0)
        {
            _isFile = isFilePath;
            _String = p;
            normalize();
        }

        public CPath(CPath path)
        {
            _isFile = path._isFile;
            _String = path._String;
        }

        public CPath(CPath path, bool isFilePath)
        {
            _isFile = isFilePath;
            _String = path._String;
        }

        /** 标准化路径:
		* 工序: 
		*		1.修剪两边的空格
		*		2.把['/']统一替换成['\']
		*/
        public void normalize()
        {
            replace_all(CHAR_SLASH, CHAR_SLASH2);
        }

        void replace_all(string substr, string str)
        {
            _String = _String.Replace(substr, str);
            /*
			int search_len=substr.Length;
			if (search_len==0)
				return;

			int pos = (int)(_String.Length-search_len); // 原来size_type是无符号的
			for (;pos>=0;)
			{
				if (string.Compare(_String, pos, substr, 0, search_len) == 0)
				//if (compare(pos,search_len,substr)==0)
				{
					replace(pos,search_len,str);
					pos -= (int)search_len;
				}
				else
				{
					--pos;
				}
			}
			*/
        }

        public string getFileName()
        {
            if (!isFile())
            {
                return _String;
            }

            int pos = getLastSlashPos();

            return _String.Substring(pos + 1, _String.Length - pos - 1);
        }

        public string getFileTitle()
        {
            if (!isFile())
            {
                return _String;
            }

            int slashPos = getLastSlashPos();
            int dotPos = getLastDotPos();
            return _String.Substring(slashPos + 1, dotPos - slashPos - 1);
        }

        public string getFileExt()
        {
            if (!isFile() || _String == string.Empty)
            {
                return _String;
            }

            int pos = getLastDotPos();
            if (pos < 0) // no ext
            {
                return _String;
            }

            return _String.Substring(pos + 1, _String.Length - pos - 1);
        }

        public string getParentDir()
        {
            if (!isFile())
                removeTailSlash();

            int pos = getLastSlashPos();
            if (pos < 0)
            {
                return _String;
            }
            return _String.Substring(0, pos);
        }

        public CPath addTailSlash()
        {
            byte[] Temp = System.Text.Encoding.UTF8.GetBytes(_String);

            if (Temp[Temp.Length - 1] != System.Text.Encoding.UTF8.GetBytes(CHAR_SLASH2)[0])
            {
                _String += CHAR_SLASH2;
            }

            return this;
        }

        public CPath removeTailSlash()
        {
            byte[] Temp = System.Text.Encoding.UTF8.GetBytes(_String);

            if (Temp[Temp.Length - 1] != System.Text.Encoding.UTF8.GetBytes(CHAR_SLASH2)[0])
            {
                _String.Remove(_String.Length - 1, 1);
            }

            return this;
        }

        public void isFile(bool isfile)
        {
            _isFile = isfile;
        }

        public bool isFile()
        {
            return _isFile;
        }

        int getLastDotPos()
        {
            return _String.LastIndexOf(CHAR_DOT);
        }

        int getLastSlashPos()
        {
            return _String.LastIndexOf(CHAR_SLASH2);
        }

        int getLastSlashPos(int count)
        {
            int pos = _String.LastIndexOf(CHAR_SLASH2);
            while (pos >= 0 && --count > 0)
            {
                pos = _String.LastIndexOf(CHAR_SLASH2, pos - 1);
                //pos =find_last_of(CHAR_SLASH, pos - 1);
            }
            return pos;
        }

        static bool stringIsNull(string strPtr)
        {
            return string.IsNullOrEmpty(strPtr);
        }

        static bool stringIsEmpty(string strPtr)
        {
            return strPtr == string.Empty;
        }

        static bool stringIsValid(string strPtr)
        {
            return !string.IsNullOrEmpty(strPtr);
        }

        /// <summary>
        /// 设置工作目录
        /// </summary>
        /// <param name="path"></param>
        public static void setWorkDir(string path)
        {
            s_workDir = path;
        }

        /// <summary>
        /// 获取工作目录
        /// </summary>
        /// <returns></returns>
        public static string getWorkDir()
        {
            if (s_workDir != string.Empty)
            {
                return s_workDir;
            }
            s_workDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //s_workDir = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //s_workDir = s_workDir.Substring(0, s_workDir.LastIndexOf('\\') + 1);
            //s_workDir = System.IO.Directory.GetCurrentDirectory();
            /*
			#ifdef WIN32
		#ifdef WinSDK_UTF8
				wchar_t wd[MAX_PATH] = {0};
				if (GetModuleFileNameW(NULL, wd, MAX_PATH - 1))
				{
					PathW filename(wd);
					sstrcpyn(s_workDir, w2utf8(filename.getParentDir().c_str()), MAX_PATH - 1);
				}
		#else

				HMODULE hCallerModule = NULL;   
				char szModuleName[MAX_PATH] = "";   
				void *callerAddress = &getWorkDir;
				if (GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS, (LPCSTR)callerAddress, &hCallerModule))   
				{   
				}
				else
				{
					hCallerModule=NULL;
				}

				char szShort[MAX_PATH];
				DWORD ret=GetModuleFileName(hCallerModule, szShort, _MAX_PATH-1);
				GetLongPathName(szShort,s_workDir,MAX_PATH-1);

				//if (GetModuleFileName(NULL, s_workDir, MAX_PATH - 1))
				if (ret)
				{
					CPath filename(s_workDir);
					sstrcpyn(s_workDir, filename.getParentDir().c_str(), MAX_PATH - 1);
				}
		#endif
			#else
				_tgetcwd(s_workDir, MAX_PATH - 1);
			#endif
			   */
            return s_workDir;
        }

        public static bool _isDirectoryExist(CPath absolutePath)
        {
            DirectoryInfo dir = new DirectoryInfo(absolutePath._String);

            return (dir.Exists && (dir.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
        }

        public static bool _isAbsolutePath(string path)
        {
            return (path.Length >= 3 && (path[0] == '/' || path[1] == ':'));
        }


        /// <summary>
        /// 切换到文件的全路径名
        /// </summary> 不检查文件或文件所在的各级目录是否存在，检查是否存在使用@c checkPath
        /// <param name="path">要转换的文件名（可以是当前目录的相对路径）</param>
        public static void ToggleFullPath(ref CPath path)
        {
            if (path._String == string.Empty)
            {
                path._String = getWorkDir();
                return;
            }

            if (!_isAbsolutePath(path._String))
            {
                CPath workPath = new CPath(getWorkDir(), false);
                //if (!Api.IsMobilePlatform())
                {
                    workPath.addTailSlash();
                }

                path._String = workPath._String + path._String;
            }

            path.normalize();
        }

        /// <summary>
        /// 检查路径是否存在（包括目录路径和文件路径）
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="isAbsolutePath">是否是绝对路径</param>
        /// <param name="attrib">返回文件（或目录）的属性</param>
        /// <returns> 成功返回true，否则返回false</returns>
        public static bool CheckPath(string path, bool isAbsolutePath, ref FileAttributes attrib)
        {
            CPath mypath = new CPath(path, false);
            ToggleFullPath(ref mypath);

            DirectoryInfo TheFolder = new DirectoryInfo(mypath._String);
            if (TheFolder == null)
            {
                return false;
            }
            attrib = TheFolder.Attributes;

            return true;
        }

        /// <summary>
        /// 检查指定的路径是否是绝对路径
        /// </summary>
        /// <param name="path">要检查的路径</param>
        /// <returns>绝对路径返回true，否则返回false</returns>
        public static bool IsAbsolutePath(string path)
        {
            return _isAbsolutePath(path);
        }

        /// <summary>
        ///  检查路径是否是文件
        /// </summary>
        /// <param name="path">要检查的路径（如果是相对路径，会自动以exe进程所在目录为当前目录进行处理）</param>
        /// <returns>成功返回true，否则如果路径不存在或者是目录则返回false</returns>
        public static bool IsFile(string path)
        {
            bool isAbsolutePath = false;
            FileAttributes attrib = 0;
            bool ret = CheckPath(path, isAbsolutePath, ref attrib);
            return (ret && ((attrib & FileAttributes.Directory) != FileAttributes.Directory));
        }

        /// <summary>
        /// 检查路径是否是目录
        /// </summary>
        /// <param name="path">要检查的路径（如果是相对路径，会自动以exe进程所在目录为当前目录进行处理）</param>
        /// <returns>成功返回true，否则如果路径不存在或者是文件则返回false</returns>
        public static bool IsDirectory(string path)
        {
            bool isAbsolutePath = false;
            FileAttributes attrib = 0;
            bool ret = CheckPath(path, isAbsolutePath, ref attrib);
            return (ret && ((attrib & FileAttributes.Directory) == FileAttributes.Directory));
        }

        // 递归创建目录(包括多级,absolutePath需绝对路径名)
        // 自动检测目录是否存在

        /// <summary>
        /// 递归创建目录
        /// </summary>
        /// 递归创建目录(包括多级,absolutePath需绝对路径名)
        //  自动检测目录是否存在
        /// <param name="?">要创建的绝对路径</param>
        /// <returns> 成功或者目录已存在返回true，否则返回false，失败后不处理可能创建了的目录</returns>
        public static bool CreateDirectoryRecursive(string absolutePath)
        {
            CPath strDir = new CPath(absolutePath, false);

            if (_isDirectoryExist(strDir))
                return true;

            // 获取父目录
            CPath strParent = new CPath(strDir.getParentDir(), false);
            if (strParent._String == string.Empty) // 目录名称错误
                return false;

            bool ret = true;
            if (strParent._String.Length > 3) // 如果长度小于3，表示为磁盘根目录
                ret = _isDirectoryExist(strParent);// 检查父目录是否存在

            if (!ret) // 父目录不存在,递归调用创建父目录
                ret = CreateDirectoryRecursive(strParent._String);

            if (ret) // 父目录存在,直接创建目录
            {
                DirectoryInfo TheFolder = new DirectoryInfo(strDir._String);
                TheFolder.Create();

                ret = TheFolder.Exists;
            }

            return ret;
        }

        /// <summary>
        /// 递归删除目录
        /// </summary>
        /// <param name="string">要删除的绝对路径</param>
        /// <returns>成功或者目录不存在返回true，否则返回false</returns>
        public static bool RemoveDirectoryRecursive(string absolutePath)
        {
            CPath strDir = new CPath(absolutePath, false);

            // 参数长度必须大于3，即不能为磁盘根目录或空白
            if (strDir._String == string.Empty || strDir._String.Length <= 3)
                return false;

            CPath strFiles = new CPath(strDir, true);

            DirectoryInfo fatherFolder = new DirectoryInfo(strFiles._String);

            if (!fatherFolder.Exists || (fatherFolder.Attributes & FileAttributes.Directory) != FileAttributes.Directory) // directory not exist, return true
                return true;

            //删除当前文件夹内文件
            FileInfo[] files = fatherFolder.GetFiles();
            foreach (FileInfo file in files)
            {
                //string fileName = file.FullName.Substring((file.FullName.LastIndexOf("\\") + 1), file.FullName.Length - file.FullName.LastIndexOf("\\") - 1);
                string fileName = file.Name;
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            //递归删除子文件夹内文件
            foreach (DirectoryInfo childFolder in fatherFolder.GetDirectories())
            {
                RemoveDirectoryRecursive(childFolder.FullName);
            }

            fatherFolder.Delete();

            return true;
        }

        public static bool _browseDir(CPath absoluteDir, CPath relativeDir, DIRPROC dir, FILEPROC file, object userdata, int mask = (int)BrowseDirMask.bdm_file | (int)BrowseDirMask.bdm_dir, bool recursive = true)
        {
            CPath strFiles = absoluteDir;

            DirectoryInfo fatherFolder = new DirectoryInfo(strFiles._String);
            if (!fatherFolder.Exists)
            {
                return false;
            }

            DirectoryInfo[] dirs = fatherFolder.GetDirectories();
            FileInfo[] files = fatherFolder.GetFiles();

            // 是目录
            foreach (DirectoryInfo elem in dirs)
            {
                if ((elem.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) // 忽略隐藏文件
                    continue;

                if ((elem.Attributes & FileAttributes.Directory) == FileAttributes.Directory) // is directory!
                {
                    if ((mask & (int)BrowseDirMask.bdm_dir) != 0 && (elem.Name.IndexOf(".") != 0) && (elem.Name.IndexOf("..") != 0)) // for ".", ".."
                    {
                        if (absoluteDir._String != string.Empty)
                            absoluteDir.addTailSlash();

                        absoluteDir._String += elem.Name;

                        if (elem.Name == relativeDir._String && dir != null && dir(absoluteDir._String, relativeDir._String, userdata))
                        {
                            return false;
                        }

                        if (recursive)
                        {
                            if (!_browseDir(absoluteDir, relativeDir, dir, file, userdata, mask, recursive))
                            {
                                return false;
                            }
                        }

                        absoluteDir._String = absoluteDir.getParentDir();
                        relativeDir._String = relativeDir.getParentDir();
                    }
                }
            }

            // 是文件
            foreach (FileSystemInfo elem in files)
            {
                if ((mask & (int)BrowseDirMask.bdm_file) == (int)BrowseDirMask.bdm_file)
                {
                    CPath strFileName = absoluteDir;
                    CPath relativeFile = relativeDir;

                    if (strFileName._String != string.Empty)
                        strFileName.addTailSlash();

                    strFileName._String += elem.Name;

                    if (elem.Name.IndexOf(relativeDir._String) >= 0 && file != null)
                    {
                        file(strFileName._String, relativeFile._String, userdata);
                        return false;
                    }
                }
            }
            return true;
        }

        public static DFP_RESULT _browseDirEx(CPath absoluteDir, CPath relativeDir, DirFileProc dirFileProc, object userdata = null, int mask = (int)BrowseDirMask.bdm_file | (int)BrowseDirMask.bdm_dir, bool recursive = true, bool browseHidden = false)
        {
            CPath strFiles = absoluteDir;

            string[] entries = Directory.GetFileSystemEntries(absoluteDir._String);

            DirectoryInfo fatherFolder = new DirectoryInfo(strFiles._String);

            if (!fatherFolder.Exists)
            {
                return DFP_RESULT.DFP_ABORT;
            }

            DirectoryInfo[] dirs = fatherFolder.GetDirectories();
            FileInfo[] files = fatherFolder.GetFiles();

            // 是目录
            foreach (DirectoryInfo dir in dirs)
            {
                if (!browseHidden && (dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    continue;

                if (((int)dir.Attributes & (int)FileAttributes.Directory) == (int)FileAttributes.Directory) // is directory!
                {
                    if (((mask & (int)BrowseDirMask.bdm_dir) != 0) && (dir.Name.IndexOf('.') != 0) && (dir.Name.IndexOf("..") != 0)) // for ".", ".."
                    {
                        if (absoluteDir._String != string.Empty)
                            absoluteDir.addTailSlash();

                        absoluteDir._String += dir.Name;

                        DFP_RESULT ret = DFP_RESULT.DFP_SUCCESS;
                        if (dir.Name == relativeDir._String)
                        {
                            ret = dirFileProc(false, absoluteDir._String, relativeDir._String, dir.Name, userdata);
                        }

                        if (ret == DFP_RESULT.DFP_ABORT)
                            return ret;

                        if (recursive && (int)ret == 1)
                        {
                            ret = _browseDirEx(absoluteDir, relativeDir, dirFileProc, userdata, mask, recursive, browseHidden);
                            if (ret == DFP_RESULT.DFP_ABORT)
                                return ret;
                        }

                        absoluteDir._String = absoluteDir.getParentDir();
                        relativeDir._String = relativeDir.getParentDir();
                    }
                }
            }

            // 是文件
            foreach (FileSystemInfo file in files)
            {
                if ((mask & (int)BrowseDirMask.bdm_file) == (int)BrowseDirMask.bdm_file)
                {
                    CPath strFileName = absoluteDir;
                    CPath relativeFile = relativeDir;

                    if (strFileName._String != string.Empty)
                        strFileName.addTailSlash();

                    strFileName._String += file.Name;

                    if (file.Name.IndexOf(relativeDir._String) >= 0)
                    {
                        DFP_RESULT ret = dirFileProc(true, strFileName._String, relativeFile._String, file.Name, userdata);
                        if (ret == DFP_RESULT.DFP_ABORT)
                            return ret;
                    }
                }
            }
            return DFP_RESULT.DFP_SUCCESS;
        }

        /// <summary>
        ///  浏览目录
        /// </summary>
        /// <param name="absoluteDir">绝对目录路径</param>
        /// <param name="relativeDir">相对目录，用于记录绝对目录下面所有的子目录的相对位置</param>
        /// <param name="dir">目录回调函数</param>
        /// <param name="file">文件回调函数</param>
        /// <param name="userdata">用户提供的私有数据</param>
        /// <param name="mask">用于浏览文件和目录的mask，mask＝0x1表示只浏览目录，mask=0x2表示只浏览文件，可进行or操作</param>
        /// <param name="recursive"></param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool BrowseDirectory(string absoluteDir, string relativeDir, DIRPROC dir, FILEPROC file, object userdata = null, int mask = (int)BrowseDirMask.bdm_file | (int)BrowseDirMask.bdm_dir, bool recursive = true)
        {
            CPath absolutePath = new CPath(absoluteDir, false);
            CPath relativePath = new CPath(relativeDir, false);

            return _browseDir(absolutePath, relativePath, dir, file, userdata, mask, recursive);
        }

        /// <summary>
        ///  浏览目录
        /// </summary>
        /// <param name="absoluteDir">绝对目录路径</param>
        /// <param name="relativeDir">相对目录，用于记录绝对目录下面所有的子目录的相对位置</param>
        /// <param name="dirFileProc">目录和文件的回调函数</param>
        /// <param name="userdata">用户提供的私有数据</param>
        /// <param name="mask"> 用于浏览文件和目录的mask，mask＝0x1表示只浏览目录，mask=0x2表示只浏览文件，可进行or操作</param>
        /// <param name="recursive"></param>
        /// <param name="browseHidden"></param>
        /// <returns> 成功返回true，否则返回false</returns>
        public static bool BrowseDirectoryEx(string absoluteDir, string relativeDir, DirFileProc dirFileProc, object userdata = null, int mask = (int)BrowseDirMask.bdm_file | (int)BrowseDirMask.bdm_dir, bool recursive = true, bool browseHidden = false)
        {
            if (absoluteDir == null || relativeDir == null || dirFileProc == null || mask == 0)
                return false;

            CPath absolutePath = new CPath(absoluteDir, false);
            if (absolutePath._String == string.Empty)
                return false;

            CPath relativePath = new CPath(relativeDir, false);

            return DFP_RESULT.DFP_ABORT != _browseDirEx(absolutePath, relativePath, dirFileProc, userdata, mask, recursive, browseHidden);
        }
    }
}
