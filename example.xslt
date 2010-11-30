<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ext="urn:custom" extension-element-prefixes="ext">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="no" />
  <xsl:param name="example"></xsl:param>

  <xsl:template match="/">
    <xsl:element name="root">
      <xsl:attribute name="param">
        <xsl:value-of select="$example"/>
      </xsl:attribute>
      <xsl:element name="output">
      <xsl:value-of select="ext:CallableFunction('XSLT Call')" />
      </xsl:element>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
