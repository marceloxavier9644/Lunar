using Microsoft.Win32;
using System.Diagnostics;

namespace LunarBase.Utilidades
{
    public class ExecutionLimiter
    {
        private const string RegistryKeyPath = @"SOFTWARE\RegisterMon\Monreg";
        private const string RegistryValueName = "ExecutionCount";
        private const int MaxExecutions = 3;
        private const string DevMachineName = "NoteMarcelo"; // Substitua pelo nome do seu computador de desenvolvimento

        public bool CanExecute()
        {
            if (Environment.MachineName.Equals(DevMachineName, StringComparison.OrdinalIgnoreCase))
            {
                // Se for o computador de desenvolvimento, pode executar sem limite
                return true;
            }

            int executionCount = GetExecutionCount();
            return executionCount < MaxExecutions;
        }

        public void IncrementExecutionCount()
        {
            int currentCount = GetExecutionCount();

            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
                {
                    key.SetValue(RegistryValueName, currentCount + 1, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao escrever no registro: {ex.Message}");
            }
        }

        private int GetExecutionCount()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, writable: true))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(RegistryValueName);
                        if (value != null && int.TryParse(value.ToString(), out int count))
                        {
                            return count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao ler o registro: {ex.Message}");
            }

            return 0;
        }
    }
}
