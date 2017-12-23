using System;

static class MathUtils
{
    public static float Bezier3( float x0, float x1, float x2, float t )
    {
        float mt = 1.0f - t;
        float mt2 = mt * mt;
        return mt2 * x0 + 2.0f * mt * t * x1 + t * t * x2;
    }

    public static float Bezier4( float p0, float p1, float p2, float p3, float t )
    {
	    float mt = 1.0f - t;
	    float mt2 = mt * mt;
	    float mt3 = mt2 * mt;
	    float t2 = t * t;
	    float t3 = t2 * t;
	    return mt3 * p0 + 3.0f * mt2 * t * p1 + 3.0f * mt * t2 * p2 + t3 * p3;
    }

    public static float Lerp( float a, float b, float f )
    {
        return b * f + a * ( 1.0f - f );
    }

    public static float MapRange( float value, float in_min, float in_max, float out_min, float out_max )
    {
        float rel = ( value - in_min ) / ( in_max - in_min );
        return MathUtils.Lerp( out_min, out_max, rel );
    }

    public static float Clamp( float a, float b, float f )
    {
        if ( f < a )
            return a;
        if ( f > b )
            return b;
        return f;
    }

    public static float Smoothstep( float edge0, float edge1, float x )
    {
        x = MathUtils.Clamp( 0.0f, 1.0f, ( x - edge0 ) / ( edge1 - edge0 ) );
        return x * x * ( 3.0f - 2.0f * x );
    }

    public static float Smootherstep( float edge0, float edge1, float x )
    {
    	x = MathUtils.Clamp( 0.0f, 1.0f, ( x - edge0 ) / ( edge1 - edge0 ) );
    	return x * x * x * ( x * ( x * 6.0f - 15.0f ) + 10.0f );
    }

    public static float GetRandom()
    {
        return (float)new Random().NextDouble();
    }

    public static float GetRandom( float min, float max )
    {
        float r = (float)new Random().NextDouble();
        return r * ( max - min ) + min;
    }

}