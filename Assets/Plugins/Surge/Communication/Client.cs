/// <summary>
/// SURGE FRAMEWORK
/// Author: Bob Berkebile
/// Email: bobb@pixelplacement.com
///
/// A super simple client for sending data back and forth.
///
/// </summary>

using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

namespace Surge
{
	public class Client : NetworkDiscovery
	{
		#region Public Events:
		public static event Action OnConnected;
		public static event Action OnDisconnected;
		public static event Action<ServerAvailableMessage> OnServerAvailable;
		public static event Action<FloatMessage> OnFloat;
		public static event Action<FloatArrayMessage> OnFloatArray;
		public static event Action<IntMessage> OnInt;
		public static event Action<IntArrayMessage> OnIntArray;
		public static event Action<Vector2Message> OnVector2;
		public static event Action<Vector2ArrayMessage> OnVector2Array;
		public static event Action<Vector3Message> OnVector3;
		public static event Action<Vector3ArrayMessage> OnVector3Array;
		public static event Action<Vector4Message> OnVector4;
		public static event Action<Vector4ArrayMessage> OnVector4Array;
		public static event Action<RectMessage> OnRect;
		public static event Action<RectArrayMessage> OnRectArray;
		public static event Action<StringMessage> OnString;
		public static event Action<StringArrayMessage> OnStringArray;
		public static event Action<ByteMessage> OnByte;
		public static event Action<ByteArrayMessage> OnByteArray;
		public static event Action<ColorMessage> OnColor;
		public static event Action<ColorArrayMessage> OnColorArray;
		public static event Action<Color32Message> OnColor32;
		public static event Action<Color32ArrayMessage> OnColor32Array;
		public static event Action<RectByteArrayMessage> OnRectByteArray;
		public static event Action<BoolMessage> OnBool;
		public static event Action<BoolArrayMessage> OnBoolArray;
		#endregion

		#region Public Variables:
		[Tooltip("Must match the server's broadcasting port.")] public int broadcastingPort;
		[Tooltip("Must match the server's quality of service.")] public QosType qualityOfService = QosType.Reliable;
		#endregion

		#region Public Properties:
		public static bool Running
		{
			get;
			private set;
		}

		public static bool Connected
		{
			get
			{
				if (_client == null)
				{
					return false;
				}
				return _client.isConnected;
			}
		}
		#endregion

		#region Private Variables:
		static private NetworkClient _client;
		#endregion

		#region Init:
		private void Reset()
		{
			broadcastingPort = 47777;
			showGUI = false;
			broadcastData = "";
		}

		private void Awake()
		{
			//setup:
			broadcastPort = broadcastingPort;

			Initialize();
			StartAsClient();

			//configurations:
			_client = new NetworkClient();

			ConnectionConfig config = new ConnectionConfig();
			config.AddChannel(qualityOfService);
			config.InitialBandwidth = 500000;

			HostTopology topology = new HostTopology(config, 1);
			_client.Configure(topology);

			//event hooks:
			_client.RegisterHandler(MsgType.Connect, HandleConnected);
			_client.RegisterHandler(MsgType.Disconnect, HandleDisconnected);
			_client.RegisterHandler((short)NetworkMsg.FloatMsg, HandleFloat);
			_client.RegisterHandler((short)NetworkMsg.FloatArrayMsg, HandleFloatArray);
			_client.RegisterHandler((short)NetworkMsg.IntMsg, HandleInt);
			_client.RegisterHandler((short)NetworkMsg.IntArrayMsg, HandleIntArray);
			_client.RegisterHandler((short)NetworkMsg.Vector2Msg, HandleVector2);
			_client.RegisterHandler((short)NetworkMsg.Vector2ArrayMsg, HandleVector2Array);
			_client.RegisterHandler((short)NetworkMsg.Vector3Msg, HandleVector3);
			_client.RegisterHandler((short)NetworkMsg.Vector3ArrayMsg, HandleVector3Array);
			_client.RegisterHandler((short)NetworkMsg.Vector4Msg, HandleVector4);
			_client.RegisterHandler((short)NetworkMsg.Vector4ArrayMsg, HandleVector4Array);
			_client.RegisterHandler((short)NetworkMsg.RectMsg, HandleRect);
			_client.RegisterHandler((short)NetworkMsg.RectArrayMsg, HandleRectArray);
			_client.RegisterHandler((short)NetworkMsg.StringMsg, HandleString);
			_client.RegisterHandler((short)NetworkMsg.StringArrayMsg, HandleStringArray);
			_client.RegisterHandler((short)NetworkMsg.ByteMsg, HandleByte);
			_client.RegisterHandler((short)NetworkMsg.ByteArrayMsg, HandleByteArray);
			_client.RegisterHandler((short)NetworkMsg.ColorMsg, HandleColor);
			_client.RegisterHandler((short)NetworkMsg.ColorArrayMsg, HandleColorArray);
			_client.RegisterHandler((short)NetworkMsg.Color32Msg, HandleColor32);
			_client.RegisterHandler((short)NetworkMsg.Color32ArrayMsg, HandleColor32Array);
			_client.RegisterHandler((short)NetworkMsg.RectByteArrayMsg, HandleRectByteArray);
			_client.RegisterHandler((short)NetworkMsg.BoolMsg, HandleBool);
			_client.RegisterHandler((short)NetworkMsg.BoolArrayMsg, HandleBoolArray);
		}
		#endregion

		#region Flow
		private void OnEnable()
		{
			Running = true;
		}

		private void OnDisable()
		{
			Running = false;
		}

		private void OnDestroy()
		{
			StopBroadcast();
		}
		#endregion

		#region Public Methods
		public static void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			_client.RegisterHandler(msgType, handler);
		}

		public static void UnregisterHandler(short msgType)
		{
			_client.UnregisterHandler(msgType);
		}

		public static void Connect(string serverIp, int serverPort)
		{
			_client.Connect(serverIp, serverPort);
		}

		public static void Disconnect()
		{
			_client.Disconnect();
		}

		public static void Send(short msgType, MessageBase message)
		{
			if (!Connected) return;
			_client.Send(msgType, message);
		}

		public static void Send(float value, string id = "")
		{
			Send((short)NetworkMsg.FloatMsg, new FloatMessage(value, id));
		}

		public static void Send(float[] value, string id = "")
		{
			Send((short)NetworkMsg.FloatArrayMsg, new FloatArrayMessage(value, id));
		}

		public static void Send(int value, string id = "")
		{
			Send((short)NetworkMsg.IntMsg, new IntMessage(value, id));
		}

		public static void Send(int[] value, string id = "")
		{
			Send((short)NetworkMsg.IntArrayMsg, new IntArrayMessage(value, id));
		}

		public static void Send(Vector2 value, string id = "")
		{
			Send((short)NetworkMsg.Vector2Msg, new Vector2Message(value, id));
		}

		public static void Send(Vector2[] value, string id = "")
		{
			Send((short)NetworkMsg.Vector2ArrayMsg, new Vector2ArrayMessage(value, id));
		}

		public static void Send(Vector3 value, string id = "")
		{
			Send((short)NetworkMsg.Vector3Msg, new Vector3Message(value, id));
		}

		public static void Send(Vector3[] value, string id = "")
		{
			Send((short)NetworkMsg.Vector3ArrayMsg, new Vector3ArrayMessage(value, id));
		}

		public static void Send(Vector4 value, string id = "")
		{
			Send((short)NetworkMsg.Vector4Msg, new Vector4Message(value, id));
		}

		public static void Send(Vector4[] value, string id = "")
		{
			Send((short)NetworkMsg.Vector4ArrayMsg, new Vector4ArrayMessage(value, id));
		}

		public static void Send(Rect value, string id = "")
		{
			Send((short)NetworkMsg.RectMsg, new RectMessage(value, id));
		}

		public static void Send(Rect[] value, string id = "")
		{
			Send((short)NetworkMsg.RectArrayMsg, new RectArrayMessage(value, id));
		}

		public static void Send(string value, string id = "")
		{
			Send((short)NetworkMsg.StringMsg, new StringMessage(value, id));
		}

		public static void Send(string[] value, string id = "")
		{
			Send((short)NetworkMsg.StringArrayMsg, new StringArrayMessage(value, id));
		}

		public static void Send(byte value, string id = "")
		{
			Send((short)NetworkMsg.ByteMsg, new ByteMessage(value, id));
		}

		public static void Send(byte[] value, string id = "")
		{
			Send((short)NetworkMsg.ByteArrayMsg, new ByteArrayMessage(value, id));
		}

		public static void Send(Color value, string id = "")
		{
			Send((short)NetworkMsg.ColorMsg, new ColorMessage(value, id));
		}

		public static void Send(Color[] value, string id = "")
		{
			Send((short)NetworkMsg.ColorArrayMsg, new ColorArrayMessage(value, id));
		}

		public static void Send(Color32 value, string id = "")
		{
			Send((short)NetworkMsg.Color32Msg, new Color32Message(value, id));
		}

		public static void Send(Color32[] value, string id = "")
		{
			Send((short)NetworkMsg.Color32ArrayMsg, new Color32ArrayMessage(value, id));
		}

		public static void Send(Rect rect, byte[] bytes, string id = "")
		{
			Send((short)NetworkMsg.RectByteArrayMsg, new RectByteArrayMessage(rect, bytes, id));
		}

		public static void Send(bool value, string id = "")
		{
			Send((short)NetworkMsg.BoolMsg, new BoolMessage(value, id));
		}

		public static void Send(bool[] value, string id = "")
		{
			Send((short)NetworkMsg.BoolArrayMsg, new BoolArrayMessage(value, id));
		}
		#endregion

		#region Event Handlers:
		private void HandleFloat(NetworkMessage message)
		{
			if (OnFloat != null) OnFloat(message.ReadMessage<FloatMessage>());
		}

		private void HandleFloatArray(NetworkMessage message)
		{
			if (OnFloatArray != null) OnFloatArray(message.ReadMessage<FloatArrayMessage>());
		}

		private void HandleInt(NetworkMessage message)
		{
			if (OnInt != null) OnInt(message.ReadMessage<IntMessage>());
		}

		private void HandleIntArray(NetworkMessage message)
		{
			if (OnIntArray != null) OnIntArray(message.ReadMessage<IntArrayMessage>());
		}

		private void HandleVector2(NetworkMessage message)
		{
			if (OnVector2 != null) OnVector2(message.ReadMessage<Vector2Message>());
		}

		private void HandleVector2Array(NetworkMessage message)
		{
			if (OnVector2Array != null) OnVector2Array(message.ReadMessage<Vector2ArrayMessage>());
		}

		private void HandleVector3(NetworkMessage message)
		{
			if (OnVector3 != null) OnVector3(message.ReadMessage<Vector3Message>());
		}

		private void HandleVector3Array(NetworkMessage message)
		{
			if (OnVector3Array != null) OnVector3Array(message.ReadMessage<Vector3ArrayMessage>());
		}

		private void HandleVector4(NetworkMessage message)
		{
			if (OnVector4 != null) OnVector4(message.ReadMessage<Vector4Message>());
		}

		private void HandleVector4Array(NetworkMessage message)
		{
			if (OnVector4Array != null) OnVector4Array(message.ReadMessage<Vector4ArrayMessage>());
		}

		private void HandleRect(NetworkMessage message)
		{
			if (OnRect != null) OnRect(message.ReadMessage<RectMessage>());
		}

		private void HandleRectArray(NetworkMessage message)
		{
			if (OnRectArray != null) OnRectArray(message.ReadMessage<RectArrayMessage>());
		}

		private void HandleString(NetworkMessage message)
		{
			if (OnString != null) OnString(message.ReadMessage<StringMessage>());
		}

		private void HandleStringArray(NetworkMessage message)
		{
			if (OnStringArray != null) OnStringArray(message.ReadMessage<StringArrayMessage>());
		}

		private void HandleByte(NetworkMessage message)
		{
			if (OnByte != null) OnByte(message.ReadMessage<ByteMessage>());
		}

		private void HandleByteArray(NetworkMessage message)
		{
			if (OnByteArray != null) OnByteArray(message.ReadMessage<ByteArrayMessage>());
		}

		private void HandleColor(NetworkMessage message)
		{
			if (OnColor != null) OnColor(message.ReadMessage<ColorMessage>());
		}

		private void HandleColorArray(NetworkMessage message)
		{
			if (OnColorArray != null) OnColorArray(message.ReadMessage<ColorArrayMessage>());
		}

		private void HandleColor32(NetworkMessage message)
		{
			if (OnColor32 != null) OnColor32(message.ReadMessage<Color32Message>());
		}

		private void HandleColor32Array(NetworkMessage message)
		{
			if (OnColor32Array != null) OnColor32Array(message.ReadMessage<Color32ArrayMessage>());
		}

		private void HandleRectByteArray(NetworkMessage message)
		{
			if (OnRectByteArray != null) OnRectByteArray(message.ReadMessage<RectByteArrayMessage>());
		}

		private void HandleBool(NetworkMessage message)
		{
			if (OnBool != null) OnBool(message.ReadMessage<BoolMessage>());
		}

		private void HandleBoolArray(NetworkMessage message)
		{
			if (OnBoolArray != null) OnBoolArray(message.ReadMessage<BoolArrayMessage>());
		}

		public override void OnReceivedBroadcast(string fromAddress, string data)
		{
			if (Connected) return;

			//ip:
			int index = fromAddress.LastIndexOf(':');
			if (index != -1) fromAddress = fromAddress.Substring(index + 1);

			//data split:
			string[] dataParts = data.Split('_');

			//port:
			int port = int.Parse(dataParts[0]);

			//id:
			//HACK: this is a fix for the broadcastData bug where Unity will combine different 
			//data if the length is different because they internally reuse this object
			string id = dataParts[1].Split(new string[] { "~!~" }, StringSplitOptions.None)[0];
			
			if (OnServerAvailable != null) OnServerAvailable(new ServerAvailableMessage(fromAddress, port, id));
		}

		private void HandleConnected(NetworkMessage message)
		{
			if (OnConnected != null) OnConnected();
		}

		private void HandleDisconnected(NetworkMessage message)
		{
			if (OnDisconnected != null) OnDisconnected();
		}
		#endregion
	}
}