using NESEmulator;
using NESEmulator.Bus;
using NESEmulator.Cartridge;
using System.Drawing;
using System.Windows.Forms;

namespace NESScreen
{
    public partial class NESView : Form
    {
        private NES nes;
        private IBus bus;

        public NESView()
        {
            InitializeComponent();
        }

        private void StartEmulation(object sender, MouseEventArgs e)
        {
            //NES_screen_output.CreateGraphics().DrawRectangle(new Pen(Color.Red), new Rectangle(0, 0, 1, 1));
            ICartridge cartridge = new NESCartridge("/rom/super-mario-bros.nes");
            bus = new NESBus();
            nes = new NES(bus);
            nes.InsertCartridge(cartridge);
            nes.Reset();
        }
    }
}
