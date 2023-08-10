using System;

namespace Co.NamingLevelsPlugin.Contracts.InterfacesUI
{
    public interface IView
    {
        /// <summary>
        /// Активно окно в данный момент или нет
        /// </summary>
        bool IsLoaded { get; }

        /// <summary>
        /// Запуск немодального окна
        /// </summary>
        void Show();

        /// <summary>
        /// Запуск модального окна
        /// </summary>
        bool? ShowDialog();

        /// <summary>
        /// Закрыть окно
        /// </summary>
        void Close();

        /// <summary>
        /// Назначить окну владельца
        /// </summary>
        /// <param name="intPtr"></param>
        void SetOwner(IntPtr intPtr);

        /// <summary>
        /// Получить дескриптор окна
        /// </summary>
        /// <returns></returns>
        IntPtr GetIntPtr();
    }
}
