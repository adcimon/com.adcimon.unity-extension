using System;
using UnityEngine;

[Serializable]
public struct Matrix3x3
{
	// Memory layout:
	//
	//                  row
	//            |  0   1   2 
	//         ---+------------
	//         0  | m00 m10 m20
	// column  1  | m01 m11 m21
	//         2  | m02 m12 m22

	// m#row#col
	public float m00;
	public float m10;
	public float m20;
	public float m01;
	public float m11;
	public float m21;
	public float m02;
	public float m12;
	public float m22;

	public Matrix3x3( float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22 )
	{
		this.m00 = m00;
		this.m01 = m01;
		this.m02 = m02;
		this.m10 = m10;
		this.m11 = m11;
		this.m12 = m12;
		this.m20 = m20;
		this.m21 = m21;
		this.m22 = m22;
	}

	public Matrix3x3( Matrix3x3 m )
	{
		m00 = m.m00;
		m10 = m.m10;
		m20 = m.m20;
		m01 = m.m01;
		m11 = m.m11;
		m21 = m.m21;
		m02 = m.m02;
		m12 = m.m12;
		m22 = m.m22;
	}

	public Matrix3x3( Vector3 column0, Vector3 column1, Vector3 column2 )
	{
		this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x;
		this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y;
		this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z;
	}

	/// <summary>
	/// Access element at [row, column].
	/// </summary>
	public float this[ int row, int column ]
	{
		get
		{
			return this[row + column * 3];
		}

		set
		{
			this[row + column * 3] = value;
		}
	}

	/// <summary>
	/// Access element at sequential index (0..8 inclusive).
	/// </summary>
	public float this[ int index ]
	{
		get
		{
			switch( index )
			{
				case 0: return m00;
				case 1: return m10;
				case 2: return m20;
				case 3: return m01;
				case 4: return m11;
				case 5: return m21;
				case 6: return m02;
				case 7: return m12;
				case 8: return m22;
				default: throw new IndexOutOfRangeException("Invalid matrix index.");
			}
		}

		set
		{
			switch( index )
			{
				case 0: m00 = value; break;
				case 1: m10 = value; break;
				case 2: m20 = value; break;
				case 3: m01 = value; break;
				case 4: m11 = value; break;
				case 5: m21 = value; break;
				case 6: m02 = value; break;
				case 7: m12 = value; break;
				case 8: m22 = value; break;
				default: throw new IndexOutOfRangeException("Invalid matrix index.");
			}
		}
	}


	/// <summary>
	/// Returns a row of the matrix.
	/// </summary>
	public Vector3 GetRow( int index )
	{
		switch( index )
		{
			case 0: return new Vector3(m00, m01, m02);
			case 1: return new Vector3(m10, m11, m12);
			case 2: return new Vector3(m20, m21, m22);
			default: throw new IndexOutOfRangeException("Invalid row index.");
		}
	}

	/// <summary>
	/// Get a column of the matrix.
	/// </summary>
	public Vector4 GetColumn( int index )
	{
		switch( index )
		{
			case 0: return new Vector3(m00, m10, m20);
			case 1: return new Vector3(m01, m11, m21);
			case 2: return new Vector3(m02, m12, m22);
			default: throw new IndexOutOfRangeException("Invalid column index.");
		}
	}

	/// <summary>
	/// Sets a row of the matrix.
	/// </summary>
	public void SetRow( int index, Vector3 row )
	{
		this[index, 0] = row.x;
		this[index, 1] = row.y;
		this[index, 2] = row.z;
	}

	/// <summary>
	/// Sets a column of the matrix.
	/// </summary>
	public void SetColumn( int index, Vector3 column )
	{
		this[0, index] = column.x;
		this[1, index] = column.y;
		this[2, index] = column.z;
	}

	/// <summary>
	/// Zero matrix.
	/// </summary>
	private static readonly Matrix3x3 zeroMatrix = new Matrix3x3(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));

	/// <summary>
	/// Returns a matrix with all elements set to zero.
	/// </summary>
	public static Matrix3x3 zero { get { return Matrix3x3.zeroMatrix; } }

	/// <summary>
	/// Identity matrix.
	/// </summary>
	private static readonly Matrix3x3 identityMatrix = new Matrix3x3(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1));

	/// <summary>
	/// Returns the identity matrix.
	/// </summary>
	public static Matrix3x3 identity { get { return Matrix3x3.identityMatrix; } }

	/// <summary>
	/// Checks whether this is an identity matrix.
	/// </summary>
	public bool isIdentity { get { return this.Equals(identity); } }

	/// <summary>
	/// The determinant of the matrix.
	/// </summary>
	public float determinant { get { return m00 * (m11 * m22 - m12 * m21) - m01 * (m10 * m22 - m12 * m20) + m02 * (m10 * m21 - m11 * m20); } }

	/// <summary>
	/// Calculates the inverse of this matrix.
	/// </summary>
	private Matrix3x3 Inverse()
	{
		float determinant = this.determinant;

		if( determinant == 0 )
		{
			throw new UnityException("Can't invert matrix with determinant 0.");
		}

		float invdet = 1f / determinant;

		Matrix3x3 output = new Matrix3x3();

		output.m00 = (m11 * m22 - m21 * m12) * invdet;
		output.m10 = (m20 * m12 - m10 * m22) * invdet;
		output.m20 = (m10 * m21 - m20 * m11) * invdet;
		output.m01 = (m21 * m02 - m01 * m22) * invdet;
		output.m11 = (m00 * m22 - m20 * m02) * invdet;
		output.m21 = (m20 * m01 - m00 * m21) * invdet;
		output.m02 = (m01 * m12 - m11 * m02) * invdet;
		output.m12 = (m10 * m02 - m00 * m12) * invdet;
		output.m22 = (m00 * m11 - m10 * m01) * invdet;

		return output;
	}

	/// <summary>
	/// The inverse of this matrix.
	/// </summary>
	public Matrix3x3 inverse { get { return Inverse(); } }

	/// <summary>
	/// Calculates the transpose of this matrix.
	/// </summary>
	private Matrix3x3 Transpose()
	{
		Matrix3x3 output = new Matrix3x3();

		output.m00 = m00;
		output.m01 = m10;
		output.m02 = m20;
		output.m10 = m01;
		output.m11 = m11;
		output.m12 = m21;
		output.m20 = m02;
		output.m21 = m12;
		output.m22 = m22;

		return output;
	}

	/// <summary>
	/// The transpose of this matrix.
	/// </summary>
	public Matrix3x3 transpose { get { return Transpose(); } }

	/// <summary>
	/// Transforms a position by this matrix.
	/// </summary>
	public Vector2 MultiplyVector2( Vector2 v )
	{
		Vector2 output = new Vector2();

		output.x = m00 * v.x + m01 * v.y + m02;
		output.y = m10 * v.x + m11 * v.y + m12;

		return output;
	}

	/// <summary>
	/// Transforms a position by this matrix.
	/// </summary>
	public Vector3 MultiplyVector3( Vector3 v )
	{
		Vector3 output = new Vector3();

		output.x = m00 * v.x + m01 * v.y + m02;
		output.y = m10 * v.x + m11 * v.y + m12;
		output.z = v.z;

		return output;
	}

	/// <summary>
	/// Multiply a matrix by this matrix.
	/// </summary>
	public Matrix3x3 MultiplyMatrix3x3( Matrix3x3 m )
	{
		Matrix3x3 output = new Matrix3x3();

		output.m00 = m00 * m.m00 + m10 * m.m01 + m20 * m.m02;
		output.m10 = m00 * m.m10 + m10 * m.m11 + m20 * m.m12;
		output.m20 = m00 * m.m20 + m10 * m.m21 + m20 * m.m22;
		output.m01 = m01 * m.m00 + m11 * m.m01 + m21 * m.m02;
		output.m11 = m01 * m.m10 + m11 * m.m11 + m21 * m.m12;
		output.m21 = m01 * m.m20 + m11 * m.m21 + m21 * m.m22;
		output.m02 = m02 * m.m00 + m12 * m.m01 + m22 * m.m02;
		output.m12 = m02 * m.m10 + m12 * m.m11 + m22 * m.m12;
		output.m22 = m02 * m.m20 + m12 * m.m21 + m22 * m.m22;

		return output;
	}

	public static Vector2 operator *( Matrix3x3 m, Vector2 v )
	{
		return m.MultiplyVector2(v);
	}

	public static Vector3 operator *( Matrix3x3 m, Vector3 v )
	{
		return m.MultiplyVector3(v);
	}

	public static Matrix3x3 operator *( Matrix3x3 m1, Matrix3x3 m2 )
	{
		return m1.MultiplyMatrix3x3(m2);
	}

	/// <summary>
	/// Used to allow Matrix3x3s to be used as keys in hash tables.
	/// </summary>
	public override int GetHashCode()
	{
		return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() << 2) ^ (GetColumn(2).GetHashCode() >> 2);
	}

	public override bool Equals( object other )
	{
		if( !(other is Matrix3x3) )
		{
			return false;
		}

		return Equals((Matrix3x3)other);
	}

	public bool Equals( Matrix3x3 other )
	{
		return GetColumn(0).Equals(other.GetColumn(0))
			&& GetColumn(1).Equals(other.GetColumn(1))
			&& GetColumn(2).Equals(other.GetColumn(2));
	}

	public override string ToString()
	{
		return string.Format("{0}\t{1}\t{2}\n{3}\t{4}\t{5}\n{6}\t{7}\t{8}", m00, m01, m02, m10, m11, m12, m20, m21, m22);
	}

	/// <summary>
	/// Creates a translation matrix.
	/// </summary>
	public static Matrix3x3 Translate( Vector2 translation )
	{
		Matrix3x3 output = new Matrix3x3();

		output.m00 = 1f;
		output.m02 = translation.x;
		output.m11 = 1f;
		output.m12 = translation.y;
		output.m22 = 1f;

		return output;
	}

	/// <summary>
	/// Creates a rotation matrix (rotation is expressed as euler angles).
	/// </summary>
	public static Matrix3x3 Rotate( float rotation )
	{
		float cos = Mathf.Cos(rotation * Mathf.Deg2Rad);
		float sin = Mathf.Sin(rotation * Mathf.Deg2Rad);

		Matrix3x3 output = new Matrix3x3();

		output.m00 = cos;
		output.m01 = sin;
		output.m10 = -sin;
		output.m11 = cos;
		output.m22 = 1f;

		return output;
	}

	/// <summary>
	/// Creates a scaling matrix.
	/// </summary>
	public static Matrix3x3 Scale( Vector2 scale )
	{
		Matrix3x3 output = new Matrix3x3();

		output.m00 = scale.x;
		output.m11 = scale.y;
		output.m22 = 1f;

		return output;
	}

	/// <summary>
	/// Creates a translation, rotation and scaling matrix (rotation is expressed as euler angles).
	/// </summary>
	public static Matrix3x3 TRS( Vector2 translation, float rotation, Vector2 scale )
	{
		float cos = Mathf.Cos(rotation * Mathf.Deg2Rad);
		float sin = Mathf.Sin(rotation * Mathf.Deg2Rad);

		Matrix3x3 output = new Matrix3x3();

		output.m00 = scale.x * cos;
		output.m01 = scale.x * -sin;
		output.m02 = translation.x;
		output.m10 = scale.y * sin;
		output.m11 = scale.y * cos;
		output.m12 = translation.y;
		output.m20 = 0;
		output.m21 = 0;
		output.m22 = 1;

		return output;
	}
}