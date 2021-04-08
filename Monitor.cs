using System;

namespace GreedySnake
{
	class Monitor
	{
        private int m_h_size;
        private int m_v_size;

        char[,] draw_buffer;

        // define the monitor left-top conern
        public int orignal_v;
        public int orignal_h;

        public Monitor(int v, int h, int orignal_v = 0, int orignal_h = 0)
        {
            this.m_v_size = v;
            this.m_h_size = h;
            this.orignal_v = orignal_v;
            this.orignal_h = orignal_h;

            draw_buffer = new char[m_v_size, m_h_size];
        }
	}
}
