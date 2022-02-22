using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using DirectShowLib;
using System.Runtime.InteropServices;

namespace Video.Models
{
	public class VideoPlayer
	{
		#region Constructors
		#endregion

		#region Fields And Properties

		// graph builder interfaces
		private IFilterGraph2 _FilterGraph;
		private IMediaControl _MediaCtrl;
		private IMediaEvent _MediaEvent;

		// Used to grab current snapshots
		ISampleGrabber _SampleGrabber = null;

		// Grab once. Used to create bitmap
		private int _VideoWidth;
		private int _VideoHeight;
		private int _Stride;
		private int _ImageSize; // In bytes

		// Event used by Media Event thread
		private ManualResetEvent _ManualResetEvent;

		// Current state of the graph (can change async)
		volatile private GraphState _State = GraphState.Stopped;

#if DEBUG
		// Allow you to "Connect to remote graph" from GraphEdit
		DsROTEntry _DsRot;
#endif

		public string FileName { get; private set; }

		#endregion

		#region Methods

		public void Play(Control control, string fileName)
		{
			FileName = fileName;
			SetupGraph(control);
		}

		private void SetupGraph(Control control)
		{
			_FilterGraph = new FilterGraph() as IFilterGraph2;
			
			var captureGraphBuilder = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;

			try
			{
				var hr = captureGraphBuilder.SetFiltergraph(_FilterGraph);
				DsError.ThrowExceptionForHR(hr);

#if DEBUG
				// Allows you to view the graph with GraphEdit File/Connect
				var DsRot = new DsROTEntry(_FilterGraph);
#endif
				// Add the filters necessary to render the file.  This function will
				// work with a number of different file types.
				IBaseFilter sourceFilter = null;
				hr = _FilterGraph.AddSourceFilter(FileName, FileName, out sourceFilter);
				DsError.ThrowExceptionForHR(hr);

				// Get the SampleGrabber interface
				_SampleGrabber = (ISampleGrabber)new SampleGrabber();
				IBaseFilter baseGrabFlt = (IBaseFilter)_SampleGrabber;

				// Configure the Sample Grabber
				ConfigureSampleGrabber(_SampleGrabber);

				// Add it to the filter
				hr = _FilterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber");
				DsError.ThrowExceptionForHR(hr);

				// Connect the pieces together, use the default renderer
				hr = captureGraphBuilder.RenderStream(null, null, sourceFilter, baseGrabFlt, null);
				DsError.ThrowExceptionForHR(hr);

				// Now that the graph is built, read the dimensions of the bitmaps we'll be getting
				SaveSizeInfo(_SampleGrabber);

				// Configure the Video Window
				IVideoWindow videoWindow = _FilterGraph as IVideoWindow;
				ConfigureVideoWindow(videoWindow, control);

				// Grab some other interfaces
				_MediaEvent = _FilterGraph as IMediaEvent;
				_MediaCtrl = _FilterGraph as IMediaControl;
			}
			finally
			{
				if (captureGraphBuilder != null)
				{
					Marshal.ReleaseComObject(captureGraphBuilder);
					captureGraphBuilder = null;
				}
			}
		}

		#endregion
	}
}